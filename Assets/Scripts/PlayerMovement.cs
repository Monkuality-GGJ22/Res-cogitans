using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x + inputX * speed, transform.position.y, transform.position.z + inputY * speed);
    }
}
