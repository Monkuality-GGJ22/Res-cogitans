using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool drawDebug;

    private float inputX; 
    private float inputY;
    private Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float baseSpeed = playerSpeed * Time.fixedDeltaTime;
        Vector3 v = Vector3.right * inputX * baseSpeed +
            Vector3.forward * inputY * baseSpeed;
        rbody.velocity = v;
        if (drawDebug) Debug.DrawLine(transform.position, transform.position + v.normalized * v.magnitude/4, Color.red, 1);
    }
}