using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDiamondBehaviour : MonoBehaviour
{
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float diamondMovingSpeed;
    [SerializeField] private float diamondHeight;

    private Light diamondLightComponent;
    private SoulIntensity soulIntensityComponent;

    private AudioSource audioSource;

    private void Start()
    {
        diamondLightComponent = gameObject.GetComponent<Light>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Diamond moves up and down on the spot
        float y = Mathf.PingPong(Time.time * diamondMovingSpeed, 1) + diamondHeight;
        gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);

        if (gameObject.GetComponent<Light>().intensity <= 0)
            Destroy(gameObject);        
    }

    private void OnTriggerStay(Collider other)
    {
        soulIntensityComponent = other.gameObject.GetComponent<SoulIntensity>();
        if (soulIntensityComponent != null && 
            other.gameObject.GetComponent<Light>().intensity < other.gameObject.GetComponent<SoulIntensity>().MaxLightIntensity)
        {
            //Decrease diamond intensity
            diamondLightComponent.intensity -= (chargingSpeed * Time.fixedDeltaTime);
            //Increase soul intensity
            other.gameObject.GetComponent<Light>().intensity += chargingSpeed * Time.fixedDeltaTime;            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SoulIntensity>())
            audioSource.Play();
    }
}
