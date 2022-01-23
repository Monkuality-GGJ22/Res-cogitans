using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool drawDebug;

    [SerializeField] private float positionStoreFrequency;

    private float inputX;
    private float inputY;
    private Rigidbody rbody;
    private Vector3 prevPosition;
    private float prevPositionTimer;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        prevPosition = Vector3.zero;
        prevPositionTimer = 0f;
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        prevPositionTimer += Time.deltaTime;
        if (prevPositionTimer >= positionStoreFrequency)
        {
            prevPositionTimer = 0f;
            if (transform.position.y >= prevPosition.y)
            {
                prevPosition = transform.position;
            }
        }
    }

    private void FixedUpdate()
    {
        float baseSpeed = playerSpeed * Time.fixedDeltaTime;
        Vector3 v = Vector3.right * inputX +
            Vector3.forward * inputY;

        if (v.sqrMagnitude > 1)
            v.Normalize();

        if (transform.position.y >= prevPosition.y)
            rbody.velocity = v * baseSpeed;

        if (drawDebug) Debug.DrawLine(transform.position, transform.position + v.normalized * v.magnitude / 4, Color.red, 1);
    }


    public void Respawn(Vector3? pos = null)
    {
        if (pos == null)
        {
            pos = prevPosition;
        }
        else
        {
            pos += Vector3.up * GetComponent<Collider>().bounds.extents.y;
            prevPosition = Vector3.zero;
        }
        transform.position = (Vector3)pos;
        prevPositionTimer = 0f;
        rbody.velocity = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        CheckpointComponent checkpoint = other.GetComponentInParent<CheckpointComponent>();
        if (checkpoint != null)
        {
            checkpoint.CheckIn();
        }
    }

}