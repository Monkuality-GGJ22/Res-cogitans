using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RemoteTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool drawLines = true;
    [SerializeField] private Color color = Color.red;
    [Header("Specifics")]
    [SerializeField] public RemoteActivation activationObject;


    private void OnDrawGizmos()
    {
        if (drawLines && activationObject != null)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, activationObject.transform.position);
        }
    }
}
