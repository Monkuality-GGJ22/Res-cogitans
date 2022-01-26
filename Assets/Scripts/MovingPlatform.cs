using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    private Transform previousParent;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startingPosition, startingPosition + offset , pingPong);
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
}