using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void MainMenuPlayClicked()
    {
        SceneManager.LoadScene("TestScene_Ricci");
        //SceneManager.LoadScene("", LoadSceneMode.Additive);
    }

    public void MainMenuExitClicked()
    {
        Application.Quit();
    }
}
