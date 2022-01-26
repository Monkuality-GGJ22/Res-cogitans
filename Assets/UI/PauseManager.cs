using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void BackToMainMenuClicked()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    public void BackToLevelSelectClicked()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
        SceneManager.LoadScene("LevelSelectScreen", LoadSceneMode.Single);
    }
    public void ResumeClicked()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }
}
