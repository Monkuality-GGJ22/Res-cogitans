using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleActivationScript : RemoteActivation
{
    private Vector3 startingPos;
    
    public override void Activate()
    {
        Debug.Log("on");
    }

    public override void Deactivate()
    {
        Debug.Log("off");
    }

    public override void Respawn()
    {
        transform.position = startingPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
