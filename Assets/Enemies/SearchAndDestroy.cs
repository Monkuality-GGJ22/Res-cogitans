using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroy : MonoBehaviour
{

    private GameObject lightBlade;
    private float lightIntesity = 5f;
    public float LightIntensity
    {
        set
        {
            lightIntesity = value;
            lightBlade.GetComponent<Light>().intensity = lightIntesity;
        }
        get
        {
            return lightIntesity;
        }
    }
    
    [SerializeField]
    private float terminatorTime = 3.0f;
    [SerializeField]
    private float parryTime = 0.3f;
    [SerializeField]
    private float parryTimeInLight = 0.5f;
    private bool isKillable = false;

    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    private float speedInLight = 200f;
    private Rigidbody rb;
    public float turnRate = 10f ;
    private GameObject player;
    private Vector3 direction;
    [SerializeField] float enemyForce = 20f;

    [SerializeField] float stunTime = 1f;
    float currentStunTime = 0f;
    Vector3 whereImGoing = Vector3.zero;
    private bool mannaggiaAMePotevoAndareAZappare = false;

    private float killingTime;
    private float toBeKilledTime;


    void Start()
    {
        lightBlade = transform.GetChild(0).gameObject;
        lightBlade.GetComponent<Light>().intensity = lightIntesity;
        player = GameObject.FindGameObjectWithTag("PlayerSearch").transform.Find("Body").gameObject;

        if (player == null)
            gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        terminatorTime = terminatorTime + Random.Range(-0.5f, +0.5f);
        killingTime = terminatorTime;
        toBeKilledTime = parryTime;
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        direction.y = 0;
        float angleToTarget = Vector3.Angle(transform.forward, direction);
        Vector3 turnAxis = Vector3.Cross(transform.forward, direction);

        transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);

        if (killingTime > 0) {
            isKillable = false;
            lightBlade.GetComponent<Light>().color = Color.red;

            killingTime -= Time.deltaTime;
        }
        else
        {
            if(toBeKilledTime > 0)
            {
                isKillable = true;
                lightBlade.GetComponent<Light>().color = Color.green;

                toBeKilledTime -= Time.deltaTime;
            }
            else
            {
                killingTime = terminatorTime;
                toBeKilledTime = parryTime;
            }
        }

        direction.Normalize();

    }

    private void FixedUpdate()
    {
        if(currentStunTime <= 0 && !mannaggiaAMePotevoAndareAZappare)
            rb.velocity = speed * direction * Time.deltaTime;
        else
        {
            mannaggiaAMePotevoAndareAZappare = false;
            if(whereImGoing != Vector3.zero)
            {
                rb.AddForce(whereImGoing * enemyForce, ForceMode.Impulse);
                whereImGoing = Vector3.zero;
            }
            currentStunTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!this.isKillable)
            {
                PlayerMovement scriptMagico = collision.gameObject.GetComponent<PlayerMovement>();
                if (!(scriptMagico.hitted || scriptMagico.invincible))
                {
                    collision.gameObject.GetComponent<LifeBehaviour>().DamagePlayer();
                    scriptMagico.imPushingyou = direction * enemyForce;
                    scriptMagico.hitted = true;
                }
            }
            else
                Destroy(gameObject);


        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            mannaggiaAMePotevoAndareAZappare = true;
            currentStunTime = stunTime;
            whereImGoing = transform.position - collision.transform.position;
            whereImGoing.Normalize();
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!this.isKillable)
            {
                PlayerMovement scriptMagico = collision.gameObject.GetComponent<PlayerMovement>();
                if (!(scriptMagico.hitted || scriptMagico.invincible))
                {
                    collision.gameObject.GetComponent<LifeBehaviour>().DamagePlayer();
                    scriptMagico.imPushingyou = direction * enemyForce;
                    scriptMagico.hitted = true;
                }
            }
            else
                Destroy(gameObject);
        }
    }

}