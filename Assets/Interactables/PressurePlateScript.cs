using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PressurePlateScript : RemoteTrigger
{
    [SerializeField] private float pressedTimer;
    [SerializeField] private Sprite off, on;

    private float timer;
    private bool remoteState;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void Start()
    {
        timer = 0f;
        remoteState = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = off;
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
                spriteRenderer.sprite = on;
            }
        }
        else
        {
            if (remoteState)
            {
                activationObject.Deactivate();
                remoteState = false;
                spriteRenderer.sprite = off;
            }
        }
    }
}
