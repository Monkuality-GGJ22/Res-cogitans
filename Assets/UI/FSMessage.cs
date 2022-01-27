using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMessage : MonoBehaviour
{
    [SerializeField] private GameObject messageCanvas;

    private void Start()
    {
        Show();
    }

    public void ShowFullscreenMessage() => Show();
    public void HideFullscreenMessage() => Hide();

    private void Show()
    {
        messageCanvas.SetActive(true);
        Time.timeScale = 0f;

    }

    private void Hide()
    {
        messageCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}


