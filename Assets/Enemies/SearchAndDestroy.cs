using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroy : MonoBehaviour
{

    [SerializeField]
    private float speed = 1000f;

    [SerializeField]
    private float parryTime = 0.3f;

    private Rigidbody rb;
    public float turnRate = 10f ;
    private float turnAngle;

    private GameObject player;

    //it can be horizontal or vertical
    //è l'asse su cui si posizionano per attaccare
    private int killingDirection; 

    void Start()
    {
        //don't ask me nothing
        player = GameObject.FindGameObjectWithTag("PlayerSearch").transform.Find("Body").gameObject;

        if (player == null)
            gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        killingDirection = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDelta = player.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, targetDelta);
        Vector3 turnAxis = Vector3.Cross(transform.forward, targetDelta);

        transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);

    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeBehaviour>().DamagePlayer();
        }
    }

}
