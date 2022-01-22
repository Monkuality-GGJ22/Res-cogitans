using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private LayerMask mask;
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] float lightHeight;

    private void Start()
    {
        mask = LayerMask.GetMask("Plane");
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            transform.position = hit.point + (Vector3.up * lightHeight);
        }

    }
}