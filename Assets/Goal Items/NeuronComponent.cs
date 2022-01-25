using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronComponent : RemoteActivation
{

    private RespawnManager manager;
    private Light light;

    public override void Activate()
    {
        light.enabled = true;
        manager.NeuronActivated();
    }

    public override void Deactivate()
    {
        Disable();
        manager.NeuronDeactivated();
    }

    public override void Respawn()
    {
        Disable();
    }

    private void Disable()
    {
        if (!manager.GetActiveCheckpointClearStatus())
            light.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RespawnManager>();
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
