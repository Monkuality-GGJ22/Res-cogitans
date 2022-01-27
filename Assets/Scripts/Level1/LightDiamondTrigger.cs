using UnityEngine;

public class LightDiamondTrigger : MonoBehaviour
{
    private bool activable;
    [SerializeField] private Canvas canvas;
    private UIBehaviour uiBehaviour;
    [TextArea(10, 20)]
    [SerializeField] private string message;

    private void Start()
    {
        activable = true;
        uiBehaviour = canvas.GetComponent<UIBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(activable)
        {
            uiBehaviour.PrintUIMessage(message);
            activable = false;
        }        
    }
}