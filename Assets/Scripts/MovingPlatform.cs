using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;    

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startingPosition, startingPosition + offset , pingPong);
    }
}