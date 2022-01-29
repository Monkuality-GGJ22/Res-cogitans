using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronComponent : RemoteActivation
{

    private RespawnManager manager;
    private Light light;
    private Animator animator;
    public override void Activate()
    {
        light.enabled = true;
        if (!manager.GetActiveCheckpointClearStatus())
            animator.SetTrigger("Activate");
        manager.NeuronActivated();
    }

    public override void Deactivate()
    {
        if(light.enabled)
        {
            Disable();
            manager.NeuronDeactivated();
            if (!manager.GetActiveCheckpointClearStatus())
                animator.SetTrigger("Deactivate");
        }
    }

    public override void Respawn()
    {
        Disable();
        animator.SetTrigger("Reset");
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
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Reset");
    }

    void Update()
    {
        
    }
}
