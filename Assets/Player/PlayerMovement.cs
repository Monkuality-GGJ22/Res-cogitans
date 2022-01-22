using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private float inputX; 
    private float inputY;

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + inputX * playerSpeed * Time.deltaTime, 
            transform.position.y, 
            transform.position.z + inputY * playerSpeed * Time.deltaTime);
    }
}