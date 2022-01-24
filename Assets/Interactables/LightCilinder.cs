using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class LightCilinder : RemoteTrigger
{
    [SerializeField] private float pressedTimer;

    private float timer;
    private bool remoteState;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<LightMovement>())
        {
            if (pressedTimer > 0f)
            {
                timer = pressedTimer;
            }
            TryRemoteAction(true);
            transform.gameObject.GetComponent<Light>().enabled = true;
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
            transform.gameObject.GetComponent<Light>().enabled = false;
        }
    }

    private void TryRemoteAction(bool activate)
    {
        if (activate)
        {
            if (!remoteState)
            {
                activationObject.Activate();
                remoteState = true;
            }
        }
        else
        {
            if (remoteState)
            {
                activationObject.Deactivate();
                remoteState = false;
            }
        }
    }
}
