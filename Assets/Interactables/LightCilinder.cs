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
    private bool remoteState;

    [SerializeField] private float lightThreshold1;
    [SerializeField] private float lightThreshold2;
    [SerializeField] private float lightThreshold3;

    

    void Start()
    {
        timer = 0f;
        remoteState = false;
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
            else if (other.gameObject.GetComponent<Light>().intensity > lightThreshold3)
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
            if (!remoteState)
            {
                //Confermo che questo switch non mi ha fatto dormire nella notte tra il 25 ed il 26 gennaio 2022, provvederò
                //ad emettere una denunzia verso Martin Lagas per danni morali a me ed a Dennis Ritchie
                switch (chargeLevel)
                {                    
                    case 3:
                        activationObject3.Activate();
                        goto case 2;
                    case 2:
                        activationObject2.Activate();
                        goto case 1;
                    case 1:
                        activationObject.Activate();
                        break;
                    default:
                        break;
                }

                remoteState = true;
            }
        }
        else
        {
            if (remoteState)
            {
                activationObject3.Deactivate();
                activationObject2.Deactivate();
                activationObject.Deactivate();

                remoteState = false;
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
