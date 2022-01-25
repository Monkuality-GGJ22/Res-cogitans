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
        if(light.enabled)
        {
            Disable();
            manager.NeuronDeactivated();
        }
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

    void Start()
    {
        manager = FindObjectOfType<RespawnManager>();
        light = GetComponent<Light>();
    }

    void Update()
    {
        
    }
}
