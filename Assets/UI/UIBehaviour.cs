using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Text soulIntensityText;
    [SerializeField] private GameObject soul;

    private GameObject UIMessageBG;
    private GameObject UIMessageText;

    [Header("Message Display Settings")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float displayTimePerWord;

    private int wordCount;
    private bool displayingMessage;

    private void Start()
    {
        displayingMessage = false;
    }

    private void Update()
    {
        soulIntensityText.text = soul.GetComponent<Light>().intensity.ToString("F2");
    }

    public void PrintUIMessage(string message)
    {
        if (!displayingMessage)
        {
            UIMessageBG = transform.Find("UIMessage").Find("MessageBG").gameObject;
            UIMessageText = transform.Find("UIMessage").Find("MessageText").gameObject;
            UIMessageText.GetComponent<Text>().text = message;

            char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
            wordCount = message.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

            StartCoroutine(Fade(wordCount));
        }
    }

    IEnumerator Fade(int wordCount)
    {
        displayingMessage = true;
        float elapsedTime = 0;        

        Color bgColor = UIMessageBG.GetComponent<Image>().color;
        Color textColor = UIMessageText.GetComponent<Text>().color;

        while(elapsedTime < fadeDuration)
        {
            bgColor.a = Mathf.Lerp(0, .8f, elapsedTime / fadeDuration);
            textColor.a = Mathf.Lerp(0, 1f, elapsedTime / fadeDuration);
            UIMessageBG.GetComponent<Image>().color = bgColor;
            UIMessageText.GetComponent<Text>().color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1 + displayTimePerWord * wordCount);
        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            bgColor.a = Mathf.Lerp(.8f, 0f, elapsedTime / fadeDuration);
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            UIMessageBG.GetComponent<Image>().color = bgColor;
            UIMessageText.GetComponent<Text>().color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        displayingMessage = false;

    }
}

