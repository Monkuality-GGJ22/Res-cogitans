using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyActivation : RemoteActivation
{
    [SerializeField] private List<RemoteActivation> activationObjects;
    private RemoteTrigger source;

    private void OnDrawGizmos()
    {
        source = GetComponent<RemoteTrigger>();
        if (source != null && source.drawLines && activationObjects != null)
        {
            Gizmos.color = source.color;
            foreach (var obj in activationObjects)
                Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }

    public override void Activate()
    {
        foreach (var obj in activationObjects)
            obj.Activate();
    }

    public override void Deactivate()
    {
        foreach (var obj in activationObjects)
            obj.Deactivate();
    }

    public override void Respawn()
    {
        Debug.LogError("Attempting to restart an activation multiplier");
    }
}
