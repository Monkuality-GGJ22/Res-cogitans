using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void MainMenuPlayClicked()
    {
        SceneManager.LoadScene("LevelSelectScreen", LoadSceneMode.Single);
    }

    public void MainMenuExitClicked()
    {
        Application.Quit();
    }
}
