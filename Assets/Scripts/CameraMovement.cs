using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float xDistanceFromPlayer; 
    [SerializeField] private float yDistanceFromPlayer; //Bigger = higher
    [SerializeField] private float zDistanceFromPlayer; //Higher = closer
    [SerializeField] private float cameraRotation;
    private Vector3 myPos;

    void Start()
    {
        myPos = new Vector3(xDistanceFromPlayer, yDistanceFromPlayer, zDistanceFromPlayer);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + myPos;
        transform.rotation = Quaternion.Euler(cameraRotation, 0f, 0f);
    }
}
