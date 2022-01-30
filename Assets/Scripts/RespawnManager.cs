using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour
{
    [HideInInspector] public List<CheckpointComponent> checkpoints = new List<CheckpointComponent>();

    [SerializeField] PlayerMovement body;
    [SerializeField] LifeBehaviour life;
    [SerializeField] SoulIntensity soul;

    private int activeCheckpoint;

    private void Start()
    {
        for (int i = 0; i < checkpoints.Count; ++i)
        {
            checkpoints[i].Manager = this;
        }
        activeCheckpoint = 0;
    }

    public void TrySetActiveCheckpoint(CheckpointComponent checkpoint)
    {
        int index = checkpoints.FindIndex(c => c.GetInstanceID() == checkpoint.GetInstanceID());
        if (index > activeCheckpoint)
        {
            activeCheckpoint = index;
            RefillPlayer();
        }
        Debug.Log($"Checkpoint reached: {index} - current active checkpoint: {activeCheckpoint}");
    }

    public bool GetActiveCheckpointClearStatus()
    {
        if (activeCheckpoint >= checkpoints.Count) return true;
        return checkpoints[activeCheckpoint].ClearedCheckpoint;
    }

    public void NeuronActivated()
    {
        if (activeCheckpoint >= checkpoints.Count) return;
        checkpoints[activeCheckpoint].OnNeuronActivated();
    }
    public void NeuronDeactivated()
    {
        if (activeCheckpoint >= checkpoints.Count) return;
        checkpoints[activeCheckpoint].OnNeuronDeactivated();
    }

    public void RefillPlayer(bool fromDeath = false)
    {
        life.RefillLife();
        soul.RefillLight(fromDeath);
    }

    public void RestartCheckpoint(bool playSound)
    {
        RefillPlayer(true);
        if (activeCheckpoint >= checkpoints.Count) return;
        body.Respawn(playSound,checkpoints[activeCheckpoint].transform.position);
        checkpoints[activeCheckpoint].ResetObjects();
    }

    public void RespawnPlayer(bool playSound)
    {
        body.Respawn(playSound);
        life.DamagePlayer();
    }

}
