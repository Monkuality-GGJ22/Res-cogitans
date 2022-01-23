using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private float lightHeight;
    [SerializeField] private int lightSpeed = 10;
    [SerializeField] private float distanceFromBody;
    [SerializeField] private bool drawDebugDir;

    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 previousPosition, newPosition;
    private Rigidbody rigidbody;


    private void Start()
    {
        mask = LayerMask.GetMask("Plane");
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        previousPosition = transform.position;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            newPosition = hit.point + (Vector3.up * lightHeight);
        }

        if (distanceFromBody > 0f)
        {
            Vector3 heightResetPosition = newPosition;
            heightResetPosition.y = body.transform.position.y;

            Vector3 bodyLocalPosition = heightResetPosition - body.transform.position;

            heightResetPosition = Vector3.ClampMagnitude(bodyLocalPosition, distanceFromBody) + body.transform.position;
            heightResetPosition.y = newPosition.y;
            newPosition = heightResetPosition;
        }
    }

    private void FixedUpdate()
    {
        var dir = newPosition - previousPosition;
        rigidbody.velocity = dir * lightSpeed * Time.fixedDeltaTime;

        if (drawDebugDir) Debug.DrawLine(previousPosition, newPosition, Color.green, 1);
    }
}