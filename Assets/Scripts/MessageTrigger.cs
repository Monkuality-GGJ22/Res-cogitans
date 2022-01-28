using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    private bool activable;
    [SerializeField] private Canvas canvas;
    private UIBehaviour uiBehaviour;
    [TextArea(10,20)]
    [SerializeField] private string message;
    private bool messageShown;

    private void Start()
    {
        activable = true;
        uiBehaviour = canvas.GetComponent<UIBehaviour>();
        messageShown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (activable && !messageShown && other.gameObject.GetComponent<PlayerMovement>())
        {
            messageShown = uiBehaviour.PrintUIMessage(message);
            if(messageShown)
                activable = false;
        }
    }
}