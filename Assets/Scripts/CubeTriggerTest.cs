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
        uiBehaviour.PrintUIMessage("This is a very long test message but longer then the one before, so we can say this is very long.\nBut I need it longer, so here's an even longer message.");
    }
}
