using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : RemoteTrigger
{
    [SerializeField] private Sprite off, on;

    private bool inRange, remoteState;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioClip pressAudioClip;
    [SerializeField] private AudioClip releaseAudioClip;
    [SerializeField] private bool stickyButton = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        inRange = false;
        remoteState = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = off;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            if (remoteState && stickyButton) return;
            remoteState = !remoteState;
            if (remoteState)
            {
                activationObject.Activate();
                spriteRenderer.sprite = on;

                //Sound effect for pressing
                audioSource.clip = pressAudioClip;
                audioSource.Play();
            }
            else
            {
                activationObject.Deactivate();
                spriteRenderer.sprite = off;

                //Sound effect for releasing
                audioSource.clip = releaseAudioClip;
                audioSource.Play();
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
