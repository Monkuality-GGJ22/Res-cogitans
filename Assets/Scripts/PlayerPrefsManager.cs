using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public int playerProgression;

    public void SavePrefs(int i)
    {
        PlayerPrefs.SetInt("LevelProgression", i);
        PlayerPrefs.Save();
        Debug.Log("Prefs saved");
    }

    public int LoadPrefs()
    {
        playerProgression = PlayerPrefs.GetInt("LevelProgression");
        Debug.Log("Prefs loaded");
        return playerProgression;        
    }

}
