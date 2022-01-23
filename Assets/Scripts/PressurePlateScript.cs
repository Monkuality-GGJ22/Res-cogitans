using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] private float pressedTimer;
    [SerializeField] private RemoteActivation activationObject;

    private float timer;
    private bool remoteState;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        remoteState = false;
    }

    // Update is called once per frame
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

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Movable"))
        {
            if (pressedTimer > 0f)
            {
                timer = pressedTimer;
            }
            TryRemoteAction(true);
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
