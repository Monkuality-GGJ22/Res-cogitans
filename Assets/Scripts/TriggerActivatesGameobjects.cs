using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivatesGameobjects : RemoteTrigger
{
    [SerializeField] private List<RemoteActivation> goList;

    private void OnTriggerEnter(Collider other)
    {
        foreach (RemoteActivation go in goList)
        {
            go.Activate();
        }
    }
}
