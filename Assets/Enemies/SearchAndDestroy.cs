using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroy : RemoteActivation
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
    public float parryTime = 0.5f;
    public float parryTimeInLight = 0.8f;
    private bool isKillable = false;
    [SerializeField]
    private float lightRecharge = 5f;

    public float actualSpeed;
    public float speed = 300f;
    [SerializeField]
    private float speedInLightDivider = 1.5f;
    public float speedInLight;
    private Rigidbody rb;
    public float turnRate = 10f ;
    private GameObject player;
    private Vector3 direction;
    [SerializeField] float enemyForce = 20f;

    [SerializeField] float stunTime = 1f;
    float currentStunTime = 0f;
    Vector3 whereImGoing = Vector3.zero;
    private bool mannaggiaAMePotevoAndareAZappare = false;
    private Animator animator;
    private int state = 0;
    private Rigidbody rbody;

    [SerializeField] private AudioClip footSteps;
    [SerializeField] private AudioClip hitEnemy;
    [SerializeField] private AudioClip hitPlayer;
    private AudioSource audioSource;

    private float killingTime;
    public float toBeKilledTime;

    public bool immaClone = false;

    [SerializeField] private ParticleSystem hitEffect;

    [SerializeField] NeuronComponent neuroneDaAttivare;

    [SerializeField] private bool startDeactivated = true;

    [SerializeField] private Transform deathGameObject;

    private bool dead = false;
    Vector3 startPosition;

    void Start()
    {   speed += Random.Range(-30f, +30f);
        speedInLight = speed / speedInLightDivider;
        actualSpeed = speed;
        lightBlade = transform.GetChild(0).gameObject;
        lightBlade.GetComponent<Light>().intensity = lightIntesity;
        player = GameObject.FindGameObjectWithTag("PlayerSearch").transform.Find("Body").gameObject;
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();

        if (player == null)
            gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        terminatorTime = terminatorTime + Random.Range(-0.5f, +0.5f);
        killingTime = terminatorTime;
        toBeKilledTime = parryTime;

        startPosition = transform.position;


        if(!immaClone)
            if (startDeactivated)
                this.Deactivate();
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        direction.y = 0;
        float angleToTarget = Vector3.Angle(transform.forward, direction);
        Vector3 turnAxis = Vector3.Cross(transform.forward, direction);

        transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);
        animateMovement(direction);
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
        if (rbody.velocity.x != 0 || rbody.velocity.z != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footSteps;
                audioSource.Play();
            }
        }
        else
           audioSource.Stop();
        direction.Normalize();

    }

    

    private void FixedUpdate()
    {
        if (currentStunTime <= 0 && !mannaggiaAMePotevoAndareAZappare)
        {

            rb.velocity = actualSpeed * direction * Time.fixedDeltaTime + new Vector3(0.0f, rb.velocity.y, 0.0f); ;
        }
        else
        {
            mannaggiaAMePotevoAndareAZappare = false;
            if (whereImGoing != Vector3.zero)
            {
                rb.AddForce(whereImGoing * enemyForce, ForceMode.Impulse);
                whereImGoing = Vector3.zero;
            }
            currentStunTime -= Time.fixedDeltaTime;
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
                    audioSource.clip = hitPlayer;
                    audioSource.Play();
                    hitEffect.gameObject.transform.LookAt(collision.gameObject.transform, Vector3.up);
                    hitEffect.Play();
                }
            }
            else
            {
                dieAndIncreaseLight();
            }


        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            mannaggiaAMePotevoAndareAZappare = true;
            currentStunTime = stunTime;
            whereImGoing = transform.position - collision.transform.position;
            whereImGoing.Normalize();
            audioSource.clip = hitEnemy;
            audioSource.Play();
            hitEffect.gameObject.transform.LookAt(collision.gameObject.transform);
            hitEffect.Play();
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
            {
                dieAndIncreaseLight();
            }
        }

}

    public void dieAndIncreaseLight()
    {
        deathGameObject.parent = null;
        deathGameObject.gameObject.SetActive(true);
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("PlayerSearch").transform.Find("Soul").gameObject;
        Light light = lightGameObject.GetComponent<Light>();
        var num = light.intensity;
        light.intensity = num + lightRecharge > lightGameObject.GetComponent<SoulIntensity>().MaxLightIntensity ?
            lightGameObject.GetComponent<SoulIntensity>().MaxLightIntensity : num + lightRecharge;

        if (neuroneDaAttivare != null)
            neuroneDaAttivare.Activate();

        if (immaClone)
        {
            GameObject spawnerGameObject = GameObject.FindGameObjectWithTag("EnemySpawner");
            if (spawnerGameObject)
            {
                spawnerGameObject.GetComponent<EnemySpawner>().onEnemyKill();
            }
        }
        dead = true;
        gameObject.SetActive(false);
    }

    public override void Activate()
    {
        if(!dead)
            gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override void Respawn()
    {
        dead = false;
        transform.position = startPosition;
        gameObject.SetActive(false);
        deathGameObject.SetParent(transform);
        deathGameObject.position = transform.position;
    }
    private void animateMovement(Vector3 direction)
    {

        //0 Idle, 1 top, 2 Right, 3 Down, 4 Left
        int tmpState = 0;

        if (direction.x > 0)
        {
            tmpState = 2;
        }
        else if (direction.x < 0)
        {
            tmpState = 4;
        }
        if (direction.y> 0)
        {
            tmpState = 1;
        }
        else if (direction.y < 0)
        {
            tmpState = 3;
        }
        if (direction.x == 0 && direction.y == 0)
        {
            tmpState = 0;
        }
        if (state != tmpState)
        {
            state = tmpState;
            switch (state)
            {
                case 1:
                    animator.SetTrigger("top");
                    break;
                case 2:
                    animator.SetTrigger("right");
                    break;
                case 3:
                    animator.SetTrigger("down");
                    break;
                case 4:
                    animator.SetTrigger("left");
                    break;
                default:
                    animator.SetTrigger("idle");
                    break;

            }
        }
    }
}
