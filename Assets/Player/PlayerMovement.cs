using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool drawDebug;

    [SerializeField] private float positionStoreFrequency;

    [Header("Combat")]
    [SerializeField] private float stunTime = 0.3f;
    [SerializeField] private float invicibilityTime = 0.5f;
    [SerializeField] private float blinkVelocity = 0.1f;
    public Vector3 imPushingyou;
    private MeshRenderer mr;
    private float blinkTimer = 0f;
    private float invincibilityTimer = 0f;
    private float hittedTimer = -10f;
    public bool hitted = false;
    public bool invincible = false;

    private float inputX;
    private float inputY;
    private Rigidbody rbody;
    private Vector3 prevPosition;
    private float prevPositionTimer;

    private AudioSource audioSource;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        rbody = GetComponent<Rigidbody>();
        prevPosition = Vector3.zero;
        prevPositionTimer = 0f;

        audioSource = GetComponent<AudioSource>();
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
        if (!hitted)
        {
            float baseSpeed = playerSpeed * Time.fixedDeltaTime;
            Vector3 v = Vector3.right * inputX +
                Vector3.forward * inputY;

            if (v.sqrMagnitude > 1)
                v.Normalize();

            if (transform.position.y >= prevPosition.y)
                rbody.velocity = v * baseSpeed;                

            if (invincible)
            {
                invincibilityTimer -= Time.fixedDeltaTime;
                if (invincibilityTimer <= 0)
                {
                    mr.enabled = true;
                    invincible = false;
                }
                else
                {
                    blinkTimer -= Time.fixedDeltaTime;
                    if(blinkTimer <= 0)
                    {
                        blinkTimer = blinkVelocity;
                        mr.enabled = !mr.enabled;
                    }
                }

            }

            if (drawDebug) Debug.DrawLine(transform.position, transform.position + v.normalized * v.magnitude / 4, Color.red, 1);
        }
        else
        {
            if (hittedTimer == -10)
                hittedTimer = stunTime;

            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {
                blinkTimer = blinkVelocity;
                mr.enabled = !mr.enabled;
            }

            if (imPushingyou != Vector3.zero)
            {
                rbody.AddForce(imPushingyou, ForceMode.Impulse);
                imPushingyou = Vector3.zero;
            }
            hittedTimer -= Time.deltaTime;

            if (hittedTimer <= 0)
            {
                hittedTimer = -10;
                hitted = false;
                invincible = true;
                invincibilityTimer = invicibilityTime;
                blinkTimer = blinkVelocity;
                mr.enabled = false;
            }
        }

        //Added footstep audio to the player movement
        if (rbody.velocity.x != 0 || rbody.velocity.z != 0)
        {
            if(!audioSource.isPlaying)
                audioSource.Play();
        }                
        else
            audioSource.Stop();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(PositionCoroutine());
        }
    }
    IEnumerator PositionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        prevPosition = transform.position;
    }
}