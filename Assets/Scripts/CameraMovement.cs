using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //[SerializeField] private float xDistanceFromPlayer;
    [SerializeField] private float yDistanceFromPlayer = 20; //Bigger = higher
    //[SerializeField] private float zDistanceFromPlayer; //Higher = closer
    //[SerializeField] private float cameraRotation;
    private Vector3 myPos;

    void Start()
    {
        //myPos = new Vector3(xDistanceFromPlayer, yDistanceFromPlayer, zDistanceFromPlayer);
        myPos = new Vector3(0, yDistanceFromPlayer, 0);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + myPos;
        //transform.rotation = Quaternion.Euler(cameraRotation, 0f, 0f);
        
    }
}
