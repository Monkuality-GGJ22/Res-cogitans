using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Text soulIntensityText;
    [SerializeField] private GameObject soul;

    // Update is called once per frame
    void Update()
    {
        soulIntensityText.text = soul.GetComponent<Light>().intensity.ToString("F2");
    }
}
