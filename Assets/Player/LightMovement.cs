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
    [SerializeField] private float distanceFromDarkCenter;
    [SerializeField] public float startupTime;

    private float lightSpeed;
    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 previousPosition, newPosition, olderPosition;
    private Rigidbody rbody;
    private SoulIntensity soulIntensity;
    private bool disabledMovement;
    private LayerMask enabledSoulLayer;
    private LayerMask disableddSoulLayer;
    private bool firstPosUpdate;
    private float startupTimer;

    private float range;

    private Bounds darkBounds;


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

        if (startupTimer < startupTime)
        {
            startupTimer += Time.deltaTime;
            ForceSetPos();
        }

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

        if (disabledMovement)
        {
            var point = darkBounds.ClosestPoint(newPosition);
            point.y = newPosition.y;
            var center = darkBounds.center;
            center.y = point.y;
            var dir = Vector3.ClampMagnitude(center - point, distanceFromDarkCenter);
            newPosition = center - dir;
        }

        
    }

    private void FixedUpdate()
    {
        //if (!disabledMovement)
        //{
        //    var dir = newPosition - previousPosition;
        //    rbody.velocity = dir * lightSpeed * Time.fixedDeltaTime;
        //}
        //else
        //{
        //    rbody.velocity = Vector3.zero;
        //}
        //olderPosition = previousPosition;
        olderPosition = previousPosition;

        previousPosition = transform.position;

        var dir = newPosition - previousPosition;
        rbody.velocity = dir * lightSpeed * Time.fixedDeltaTime;

        if (drawDebugDir) Debug.DrawLine(previousPosition, newPosition, Color.green, 1);

        print(newPosition + "\n");
        print(previousPosition + "\n");
        print(olderPosition + "\n");

        if (newPosition != null && olderPosition != previousPosition)
        {
            print("ciao");
            
            soulIntensity.MovingFactor = Vector3.Distance(olderPosition, previousPosition);
            print(soulIntensity.MovingFactor);
        }
        else
        {
            soulIntensity.MovingFactor = 0f;
        }

    }

    public void ForceSetPos()
    {
        Vector3 heightResetPosition = body.transform.position;
        heightResetPosition.y = previousPosition.y;
        transform.position = heightResetPosition;
    }

    public void DisableMovement(Bounds bounds)
    {
        disabledMovement = true;
        darkBounds = bounds;
        //gameObject.layer = disableddSoulLayer;
    }
    public void EnableMovement()
    {
        disabledMovement = false;
        //gameObject.layer = enabledSoulLayer;
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