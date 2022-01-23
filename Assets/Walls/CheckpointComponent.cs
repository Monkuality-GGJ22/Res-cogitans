using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    public List<Resettable> resettableObjects;

    public RespawnManager Manager { set { manager = value; } get { return manager; } }
    private RespawnManager manager;
    

    public void CheckIn()
    {
        if (manager == null) Debug.LogError("Unassigned checkpoint. Assign it to the Respawn Manager");
        else manager.TrySetActiveCheckpoint(this);
    }

    public void ResetObjects()
    {
        for (int i = 0; i < resettableObjects.Count; ++i)
        {
            resettableObjects[i].Respawn();
        }
    }
}
