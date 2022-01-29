using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefsContainer;
    [SerializeField] private GameObject hstextBox;
    private PlayerPrefsManager playerPrefsScript;
    private int highScore;

    private void Start()
    {
        AudioListener.pause = false;
        playerPrefsScript = playerPrefsContainer.GetComponent<PlayerPrefsManager>();
        highScore = playerPrefsScript.GetHighScore();
        hstextBox.GetComponent<Text>().text = "High Score: "+highScore;
        
    }

    public void Level1Clicked()
    {
        SceneManager.LoadScene("TestScene_Ricci", LoadSceneMode.Single);
    }

    public void Level2Clicked()
    {
        SceneManager.LoadScene("TestScene_Ricci", LoadSceneMode.Single);
    }

    public void LevelBrawlClicked()
    {
        SceneManager.LoadScene("LevelBrawl", LoadSceneMode.Single);
    }



    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

}
