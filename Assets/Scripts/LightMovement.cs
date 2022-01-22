using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.y = -20.8f;
        transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
    }
}