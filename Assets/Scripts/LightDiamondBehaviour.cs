using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDiamondBehaviour : MonoBehaviour
{
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float diamondMovingSpeed;
    [SerializeField] private float diamondHeight;

    private Light diamondLightComponent;
    private Light soulLightComponent;

    private void Start()
    {
        diamondLightComponent = gameObject.GetComponent<Light>();
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
        if (other.gameObject.GetComponent<LightMovement>() != null) //&& soul intensity < max intensity??
        {
            soulLightComponent = other.gameObject.GetComponent<Light>();
            //Decrease diamond intensity
            diamondLightComponent.intensity -= (chargingSpeed * Time.fixedDeltaTime);
            //Increase soul intensity
            soulLightComponent.intensity += chargingSpeed * Time.fixedDeltaTime;            
        }
    }
}
