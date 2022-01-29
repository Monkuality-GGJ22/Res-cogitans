using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{

    private void Start()
    {
        AudioListener.pause = false;
    }
    public void MainMenuPlayClicked()
    {
        SceneManager.LoadScene("LevelSelectScreen", LoadSceneMode.Single);
    }

    public void MainMenuExitClicked()
    {
        Application.Quit();
    }
}
