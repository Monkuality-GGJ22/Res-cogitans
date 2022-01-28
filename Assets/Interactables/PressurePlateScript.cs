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

    [SerializeField] private AudioClip pressAudioClip;
    [SerializeField] private AudioClip releaseAudioClip;
    private AudioSource audioSource;

    public void Start()
    {
        timer = 0f;
        remoteState = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = off;

        audioSource = GetComponent<AudioSource>();        
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

                //Sound effect for pressing
                audioSource.clip = pressAudioClip;
                audioSource.Play();
            }
        }
        else
        {
            if (remoteState)
            {
                activationObject.Deactivate();
                remoteState = false;
                spriteRenderer.sprite = off;

                //Sound effect for releasing
                audioSource.clip = releaseAudioClip;
                audioSource.Play();
            }
        }
    }
}
