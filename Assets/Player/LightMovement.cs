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
    private bool disabledMovement;
    private LayerMask enabledSoulLayer;
    private LayerMask disableddSoulLayer;
    private bool firstPosUpdate;

    private float range;


    private void Awake()
    {
        enabledSoulLayer = gameObject.layer;
        disableddSoulLayer = LayerMask.NameToLayer("Disabled Soul");
        EnableMovement();
    }

    private void Start()
    {
        mask = LayerMask.GetMask("Plane");
        rbody = GetComponent<Rigidbody>();
        soulIntensity = GetComponent<SoulIntensity>();
        range = soulIntensity.MaxLightIntensity - soulIntensity.MinLightIntensity;
        lightSpeed = minLightSpeed;
        firstPosUpdate = false;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        previousPosition = transform.position;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            newPosition = hit.point + (Vector3.up * lightHeight);
            if (!firstPosUpdate)
            {
                firstPosUpdate = true;
                previousPosition = newPosition;
                transform.position = newPosition;
            }
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
        if (!disabledMovement)
        {
            var dir = newPosition - previousPosition;
            rbody.velocity = dir * lightSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rbody.velocity = Vector3.zero;
        }

        if (drawDebugDir) Debug.DrawLine(previousPosition, newPosition, Color.green, 1);
    }

    public void DisableMovement()
    {
        disabledMovement = true;
        gameObject.layer = disableddSoulLayer;
    }
    public void EnableMovement()
    {
        disabledMovement = false;
        gameObject.layer = enabledSoulLayer;
    }

    public LayerMask GetLayer(bool enabledLayer)
    {
        return enabledLayer ? enabledSoulLayer : disableddSoulLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SearchAndDestroy script = other.gameObject.GetComponent<SearchAndDestroy>();
            script.toBeKilledTime = script.parryTimeInLight;
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            float correctedValue = soulIntensity.GetSoulIntensity() - soulIntensity.MinLightIntensity;
            float multiplier = ((correctedValue) / range) + 1;
            SearchAndDestroy script = other.gameObject.GetComponent<SearchAndDestroy>();
            script.actualSpeed = script.speed / multiplier;
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            SearchAndDestroy script = collision.gameObject.GetComponent<SearchAndDestroy>();
            script.actualSpeed = script.speed;
            script.toBeKilledTime = script.parryTime;
        }
    }

}