using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public int playerProgression;
    public int hscore;

    public void SavePrefs(int i)
    {
        PlayerPrefs.SetInt("LevelProgression", i);
        PlayerPrefs.Save();
    }

    public int LoadPrefs()
    {
        playerProgression = PlayerPrefs.GetInt("LevelProgression");
        return playerProgression;        
    }

    public void SetHighScore(int i)
    {
        PlayerPrefs.SetInt("HighScore", i);
        PlayerPrefs.Save();
    }

    public int GetHighScore()
    {
        hscore = PlayerPrefs.GetInt("HighScore");
        return hscore;
    }

}
