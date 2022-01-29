using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseCanvas;
    //[SerializeField] private GameObject resumeButton;

    //private AudioSource audioSource;
    [SerializeField] private AudioSource mainTheme;

    private AudioSource[] allAudioSources;

    private void Start()
    {
        //audioSource = resumeButton.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void BackToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        AudioListener.pause = false;
    }
        

    public void LevelSelectClicked()
    {
        SceneManager.LoadScene("LevelSelectScreen", LoadSceneMode.Single);
        AudioListener.pause = false;
    }

    //public IEnumerator ChangeSceneCoroutine(string sceneName)
    //{
    //    Time.timeScale = 1;
    //    var temp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    //    temp.allowSceneActivation = false;
    //    audioSource.Play();
    //    yield return new WaitForSeconds(audioSource.clip.length);
    //    temp.allowSceneActivation = true;
    //}

    private void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
            //mainTheme.Pause();

            AudioListener.pause = true;

            //allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            //foreach (AudioSource audioS in allAudioSources)
            //{
            //    if (audioS != mainTheme)
            //    {
            //        audioS.Stop();
            //    }
            //}
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            //mainTheme.Play();
            AudioListener.pause = false;
        }
    }

    public void ResumeClicked()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }    
}