using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : RemoteTrigger
{
    [SerializeField] private Sprite off, on;

    private bool inRange, remoteState;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void Start()
    {
        inRange = false;
        remoteState = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = off;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            remoteState = !remoteState;
            if (remoteState)
            {
                activationObject.Activate();
                spriteRenderer.sprite = on;
            }
            else
            {
                activationObject.Deactivate();
                spriteRenderer.sprite = off;
            }
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
