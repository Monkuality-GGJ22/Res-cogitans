using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCylinderTrigger : MonoBehaviour
{
    private bool activable;
    [SerializeField] private Canvas canvas;
    private UIBehaviour uiBehaviour;

    private void Start()
    {
        activable = true;
        uiBehaviour = canvas.GetComponent<UIBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activable)
        {
            uiBehaviour.PrintUIMessage("You can recharge your soul by collecting soul diamonds with it.");
            activable = false;
        }
    }
}