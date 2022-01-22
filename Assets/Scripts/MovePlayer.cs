using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float speed = 0.1f;
   

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x + inputX * speed, transform.position.y, transform.position.z + inputY * speed);
    }
}
