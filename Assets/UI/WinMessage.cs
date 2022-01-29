using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMessage : MonoBehaviour
{
    [SerializeField] private GameObject messageCanvas;

    private void Start()
    {
        
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

    public void MainMenu()
    {
        HideFullscreenMessage();
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}


