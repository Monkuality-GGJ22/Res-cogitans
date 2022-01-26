using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class LightCilinder : RemoteTrigger
{

    [SerializeField] private RemoteActivation activationObject2;
    [SerializeField] private RemoteActivation activationObject3;

    private int chargeLevel = 0;

    [SerializeField] private float pressedTimer;

    private float timer;
    private bool remoteState1;
    private bool remoteState2;
    private bool remoteState3;

    [SerializeField] private float lightThreshold1;
    [SerializeField] private float lightThreshold2;
    [SerializeField] private float lightThreshold3;

    

    void Start()
    {
        timer = 0f;
        remoteState1 = false;
        remoteState2 = false;
        remoteState3 = false;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                TryRemoteAction(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<LightMovement>())
        {
            if (pressedTimer > 0f)
            {
                timer = pressedTimer;
            }
            transform.gameObject.GetComponent<Light>().enabled = true;

            //The light intensity of the cylinder depends on the intensity of the soul (for now it uses the same values as the thresholds)
            if(other.gameObject.GetComponent<Light>().intensity <= lightThreshold1)
            {
                transform.gameObject.GetComponent<Light>().intensity = lightThreshold1;
                chargeLevel = 1;
            }
            else if (other.gameObject.GetComponent<Light>().intensity > lightThreshold1 && other.gameObject.GetComponent<Light>().intensity <= lightThreshold2)
            {
                transform.gameObject.GetComponent<Light>().intensity = lightThreshold2;
                chargeLevel = 2;
            } 
            else
            {
                transform.gameObject.GetComponent<Light>().intensity = lightThreshold3;
                chargeLevel = 3;
            }
            TryRemoteAction(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<LightMovement>())
        {
            if (pressedTimer > 0f)
            {
                timer = pressedTimer;
            }
            TryRemoteAction(false);
            chargeLevel = 0;
            transform.gameObject.GetComponent<Light>().enabled = false;
        }
    }

    private void TryRemoteAction(bool activate)
    {
        if (activate)
        {
            switch (chargeLevel)
            {                    
                case 3:
                    if (!remoteState3)
                    {
                        activationObject3.Activate();
                        remoteState3 = true;
                    }
                    goto case 2;
                case 2:
                    if (!remoteState2)
                    {
                        activationObject2.Activate();
                        remoteState2 = true;
                    }
                    goto case 1;
                case 1:
                    if (!remoteState1)
                    {
                        activationObject.Activate();
                        remoteState1 = true;
                    }
                    if (chargeLevel < 3 && remoteState3)
                    {
                        activationObject3.Deactivate();
                        remoteState3 = false;
                    }
                    if (chargeLevel < 2 && remoteState2)
                    {
                        activationObject2.Deactivate();
                        remoteState2 = false;
                    }
                    break;
                default:
                    if (remoteState3) activationObject3.Deactivate();
                    if (remoteState2) activationObject2.Deactivate();
                    if (remoteState1) activationObject.Deactivate();


                    remoteState3 = false;
                    remoteState2 = false;
                    remoteState1 = false;
                    break;
            }
        }
        else
        {
            if (remoteState3)
            {
                activationObject3.Deactivate();
                remoteState3 = false;
            }
            if (remoteState2)
            {
                activationObject2.Deactivate();
                remoteState2 = false;
            }
            if (remoteState1)
            {
                activationObject.Deactivate();
                remoteState1 = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawLines && activationObject != null && activationObject2 != null && activationObject3 != null)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, activationObject.transform.position);
            Gizmos.DrawLine(transform.position, activationObject2.transform.position);
            Gizmos.DrawLine(transform.position, activationObject3.transform.position);
        }
    }
}
