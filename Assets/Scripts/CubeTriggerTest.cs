using UnityEngine;

public class CubeTriggerTest : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private UIBehaviour uiBehaviour;

    private void Start()
    {
        uiBehaviour = canvas.gameObject.GetComponent<UIBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        uiBehaviour.PrintUIMessage("Test message the longest of messages holy moly so long");
    }
}
