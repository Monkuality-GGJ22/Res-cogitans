using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    public List<Resettable> resettableObjects;
    public List<NeuronComponent> neurons;
    public RemoteActivation endWall;

    public RespawnManager Manager { set { manager = value; } get { return manager; } }
    private RespawnManager manager;
    private int activeNeurons;

    public bool ClearedCheckpoint { get { return clearedCheckpoint; } }
    private bool clearedCheckpoint;

    private void Start()
    {
        activeNeurons = 0;
        clearedCheckpoint = false;
    }

    public void CheckIn()
    {
        if (manager == null) Debug.LogError("Unassigned checkpoint. Assign it to the Respawn Manager");
        else manager.TrySetActiveCheckpoint(this);
    }

    public void ResetObjects()
    {
        if (clearedCheckpoint) return;

        int i;
        for (i= 0; i < resettableObjects.Count; ++i)
        {
            if (resettableObjects[i] != null)
                resettableObjects[i].Respawn();
        }
        for (i = 0; i < neurons.Count; ++i)
        {
            if (neurons[i] != null)
                neurons[i].Respawn();
        }
    }


    public void OnNeuronActivated()
    {
        if (clearedCheckpoint) return;
        
        activeNeurons++;
        if (activeNeurons >= neurons.Count)
        {
            clearedCheckpoint = true;
            if (endWall != null)
            {
                endWall.Activate();
            }
            else
            {
                Debug.LogError("No End Wall referenced on the checkpoint");
            }
        }

    }

    public void OnNeuronDeactivated()
    {
        if (clearedCheckpoint) return;

        activeNeurons--;
        if (activeNeurons < 0) activeNeurons = 0;
    }
}
