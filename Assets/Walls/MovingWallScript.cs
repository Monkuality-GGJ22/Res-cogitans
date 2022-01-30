using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallScript : RemoteActivation
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector3 finalPosOffset;
    [SerializeField] private bool allowDeactivation;
    
    private Vector3 initialPos;
    private float animationTimer;
    private float endTime;
    private bool direction;

    private AudioSource audioSource;

    public override void Activate()
    {
        if (direction == true) return;
        if (animationTimer < 0f)
            animationTimer = 0f;
        direction = true;
        //Plays the audio track in the right way
        audioSource.pitch = 1;
        audioSource.time = 0f;
        audioSource.Play();
    }

    public override void Deactivate()
    {
        if (allowDeactivation && direction)
        {
            if (animationTimer < 0f) animationTimer = endTime;
            direction = false;
            //Plays the audio track in reverse
            audioSource.pitch = -1;
            audioSource.time = audioSource.clip.length - 0.001f;
            audioSource.Play();
        }
    }

    public override void Respawn()
    {
        transform.position = initialPos;
        animationTimer = -1f;
        direction = false;
    }

    private void Start()
    {
        initialPos = transform.position;
        animationTimer = -1f;
        endTime = curve[curve.length - 1].time;
        direction = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       if (animationTimer >= 0f)
       {
            animationTimer += direction ? Time.deltaTime : -Time.deltaTime;

            transform.position = Vector3.Lerp(initialPos, initialPos + finalPosOffset, curve.Evaluate(animationTimer));

            if (animationTimer > endTime) animationTimer = -1f;
        }
    }
}
