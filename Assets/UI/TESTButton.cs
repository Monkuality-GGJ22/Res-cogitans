using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTButton : MonoBehaviour
{

    public PlayerPrefsManager pla;

    void Update()
    {
        int prefs = pla.LoadPrefs();
        GetComponent<Text>().text = prefs.ToString();
    }
}
