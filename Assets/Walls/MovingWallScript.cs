using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallScript : RemoteActivation
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector3 finalPosOffset;
    
    private Vector3 initialPos;
    private float animationTimer;

    public override void Activate()
    {
        StartCoroutine(MoveCoroutine());
    }

    public override void Deactivate()
    {
    }

    public override void Respawn()
    {
        transform.position = initialPos;
        animationTimer = 0f;
    }

    private void Start()
    {
        initialPos = transform.position;
        animationTimer = 0f;
    }

    private IEnumerator MoveCoroutine()
    {
        animationTimer = 0f;

        while (animationTimer < curve[curve.length-1].time)
        {
            animationTimer += Time.deltaTime; 

            transform.position = Vector3.Lerp(initialPos, initialPos + finalPosOffset, curve.Evaluate(animationTimer));

            yield return null;
        }
    }

}
