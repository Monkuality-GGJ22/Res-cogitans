using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableComponent : Resettable
{
    private Vector3 startingPos;
    
    public override void Respawn()
    {
        transform.position = startingPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }
}
