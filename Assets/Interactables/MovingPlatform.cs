using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : RemoteActivation
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    [SerializeField] private bool startActive = true;
    private bool active;
    private bool direction;
    private float amount;

    private Transform previousParent;

    private void Start()
    {
        startingPosition = transform.position;
        active = startActive;
    }

    private void Update()
    {
        if (active)
        {
            amount += (direction ? Time.deltaTime : -Time.deltaTime) * speed;
            if (amount < 0f)
            {
                amount = 0f;
                direction = !direction;
            }
            else if (amount > 1f)
            {
                amount = 1f;
                direction = !direction;
            }
            transform.position = Vector3.Lerp(startingPosition, startingPosition + offset, amount);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            previousParent = other.gameObject.transform.parent;
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            other.gameObject.transform.parent = previousParent;
            previousParent = null;
        }
    }

    public override void Activate()
    {
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
    }

    public override void Respawn()
    {
        transform.position = startingPosition;
        active = startActive;
        amount = 0f;
    }
}