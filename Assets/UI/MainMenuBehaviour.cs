using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject goPlayButton;
    [SerializeField] private GameObject quitButton;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = goPlayButton.GetComponent<AudioSource>();
        Debug.Log(audioSource);
    }
    public void MainMenuPlayClicked() => StartCoroutine(PlayCoroutine());
    
    public IEnumerator PlayCoroutine()
    {
        var temp = SceneManager.LoadSceneAsync("LevelSelectScreen", LoadSceneMode.Single);
        temp.allowSceneActivation = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        temp.allowSceneActivation = true;
    }

    public void MainMenuExitClicked() => StartCoroutine(QuitCoroutine());

    public IEnumerator QuitCoroutine()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Application.Quit();
    }  
}
