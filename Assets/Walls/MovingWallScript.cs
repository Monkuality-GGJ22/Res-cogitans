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
    

    public override void Activate()
    {
        if (animationTimer < 0f)
            animationTimer = 0f;
        direction = true;
    }

    public override void Deactivate()
    {
        if (allowDeactivation)
        {
            if (animationTimer < 0f) animationTimer = endTime;
            direction = false;
        }
    }

    public override void Respawn()
    {
        transform.position = initialPos;
        animationTimer = -1f;
    }

    private void Start()
    {
        initialPos = transform.position;
        animationTimer = -1f;
        endTime = curve[curve.length - 1].time;
        direction = true;
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
