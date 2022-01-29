using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDiamondBehaviour : MonoBehaviour
{
    [SerializeField] private float diamondIntensity;
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float rechargingSpeed;
    [SerializeField] private float diamondMovingSpeed;
    [SerializeField] private float diamondHeight;
    [SerializeField] private bool rechargeable;
    private bool isRecharging = true;

    private Light diamondLightComponent;
    private SoulIntensity soulIntensityComponent;

    private AudioSource audioSource;

    private void Start()
    { 
        diamondLightComponent = gameObject.GetComponent<Light>();
        diamondLightComponent.intensity = diamondIntensity > 0 ? diamondIntensity : diamondLightComponent.intensity;
        audioSource = gameObject.GetComponent<AudioSource>();
        if (!rechargeable)
            diamondIntensity = 0;
    }

    private void Update()
    {
        //Diamond moves up and down on the spot
        float y = Mathf.PingPong(Time.time * diamondMovingSpeed, 1) + diamondHeight;
        gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        if (isRecharging)
        {
            diamondLightComponent.intensity = diamondLightComponent.intensity > diamondIntensity ?
                diamondLightComponent.intensity :
                diamondLightComponent.intensity + rechargingSpeed * Time.deltaTime;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        soulIntensityComponent = other.gameObject.GetComponent<SoulIntensity>();
        if (soulIntensityComponent != null && 
            other.gameObject.GetComponent<Light>().intensity < other.gameObject.GetComponent<SoulIntensity>().MaxLightIntensity &&
            diamondLightComponent.intensity > 0)
        {
            isRecharging = false;
            //Decrease diamond intensity
            diamondLightComponent.intensity -= (chargingSpeed * Time.fixedDeltaTime);
            //Increase soul intensity
            other.gameObject.GetComponent<Light>().intensity += chargingSpeed * Time.fixedDeltaTime;            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SoulIntensity>())
        {
            audioSource.Play();
            isRecharging = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SoulIntensity>())
            isRecharging = true;
    }

}
