using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightMovement : MonoBehaviour
{   
    public float LightSpeed
    {
        get { return lightSpeed; }
        set { lightSpeed=value; }
    }
    public float MaxLightSpeed
    {
        get { return maxLightSpeed; }
        set { maxLightSpeed = value; }
    }
    public float MinLightSpeed
    {
        get { return minLightSpeed; }
        set { minLightSpeed = value; }
    }

    [SerializeField] private GameObject body;
    [SerializeField] private float lightHeight;
    [SerializeField] private float maxLightSpeed;
    [SerializeField] private float minLightSpeed;
    [SerializeField] private float distanceFromBody;
    [SerializeField] private bool drawDebugDir;

    private float lightSpeed;
    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 previousPosition, newPosition;
    private Rigidbody rbody;
    private SoulIntensity soulIntensity;


    private void Start()
    {
        mask = LayerMask.GetMask("Plane");
        rbody = GetComponent<Rigidbody>();
        soulIntensity = GetComponent<SoulIntensity>();
        lightSpeed = minLightSpeed;
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
        if (newPosition != null && newPosition!= previousPosition) {
            soulIntensity.MovingFactor = Vector3.Distance(newPosition, previousPosition); 
        }
        else
        {
            soulIntensity.MovingFactor = 1f;
        }

    }

    private void FixedUpdate()
    {
        var dir = newPosition - previousPosition;
        rbody.velocity = dir * lightSpeed * Time.fixedDeltaTime;

        if (drawDebugDir) Debug.DrawLine(previousPosition, newPosition, Color.green, 1);
    }
}