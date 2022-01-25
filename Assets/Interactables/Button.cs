using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : RemoteTrigger
{
    private bool inRange, remoteState;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            remoteState = !remoteState;
            if (remoteState)
                activationObject.Activate();
            else
                activationObject.Deactivate();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
