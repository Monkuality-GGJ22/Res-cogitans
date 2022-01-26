using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBehaviour : MonoBehaviour
{
    public void Level1Clicked()
    {
        SceneManager.LoadScene("TestScene_Ricci", LoadSceneMode.Single);
    }

    public void BackToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
