using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : RemoteActivation
{
    [SerializeField] private RemoteActivation activationObject;
    private RemoteTrigger source;

    private void OnDrawGizmos()
    {
        source = GetComponent<RemoteTrigger>();
        if (source != null && source.drawLines && activationObject != null)
        {
            Gizmos.color = source.color;
            Gizmos.DrawLine(transform.position, activationObject.transform.position);
        }
    }

    public override void Activate()
    {
        activationObject.Deactivate();
    }

    public override void Deactivate()
    {
        activationObject.Activate();
    }

    public override void Respawn()
    {
        Debug.LogError("Attempting to restart an inverter");
    }
}
