using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject lvl1Button;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = lvl1Button.GetComponent<AudioSource>();
    }

    public void Level1Clicked() => StartCoroutine(SceneCoroutine("TestScene_Ricci"));
    public void Level2Clicked() => StartCoroutine(SceneCoroutine("TestScene_Ricci"));
    public void Level3Clicked() => StartCoroutine(SceneCoroutine("TestScene_Ricci"));
    public void MainMenuClicked() => StartCoroutine(SceneCoroutine("MainMenuScene"));

    public IEnumerator SceneCoroutine(string sceneName)
    {
        var temp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        temp.allowSceneActivation = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        temp.allowSceneActivation = true;
    }
}
