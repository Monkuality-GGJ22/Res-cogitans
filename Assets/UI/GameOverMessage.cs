using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject menuButton;
    private Text textBox;
    [SerializeField] private GameObject brawl;
    private BrawlController brawlController;
    [SerializeField] private PlayerPrefsManager playerPrefsManager;
    private bool newHighScore;
    GameObject newHigh;

    private void Start()
    {
        textBox = gameOverCanvas.transform.Find("IntroText").GetComponent<Text>();
        brawlController = brawl.GetComponent<BrawlController>();
        playerPrefsManager = gameOverCanvas.transform.Find("PlaPrefsContainer").GetComponent<PlayerPrefsManager>();
        newHighScore = false;
        newHigh = gameOverCanvas.transform.Find("NewHighScore").gameObject;
    }

    public void ShowFullscreenMessage()
    {
        if(brawlController.currentPoints > playerPrefsManager.GetHighScore())
        {
            playerPrefsManager.SetHighScore(brawlController.currentPoints);
            newHighScore = true;
        }
        
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        AudioListener.pause = true;

        textBox.text = "Your score: "+ brawlController.currentPoints+"\n\n\nHigh score: "+playerPrefsManager.GetHighScore();
        if(newHighScore)
            newHigh.SetActive(true);
    }

    public void MainMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;        
        newHigh.SetActive(false);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);        
    }
}