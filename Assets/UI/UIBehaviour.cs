using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class UIBehaviour : MonoBehaviour
{
    private GameObject UIMessageBG;
    private GameObject UIMessageText;
    [Header("Message Display Settings")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float displayTimePerWord;
    private int wordCount;
    private bool displayingMessage;

    [Header("Light Slider settings")]
    public Slider lightSlider;
    [SerializeField] private Text soulIntensityText;
    [SerializeField] private GameObject soul;
     private SoulIntensity soulIntensity;

    [Header("Health Slider settings")]
    public Slider healthSlider;
    [SerializeField] private Text healthLifesText;
    [SerializeField] private GameObject body;
    private LifeBehaviour lifeBehaviour;

    [Header("Checkpoint Message Settings")]
    [SerializeField] private float checkpointDisplayTime;
    [SerializeField] private float fadeDurationCheckpoint;
    private GameObject checkpointMessageBG;
    private GameObject checkpointMessageText;

    private void Start()
    {
        displayingMessage = false;

        soulIntensity = soul.GetComponent<SoulIntensity>();
        lightSlider.maxValue = soulIntensity.MaxLightIntensity;

        lifeBehaviour = body.GetComponent<LifeBehaviour>();
        healthSlider.maxValue = lifeBehaviour.maxHealth;
    }

    private void Update()
    {
        soulIntensityText.text = soul.GetComponent<Light>().intensity.ToString("F2");
        healthLifesText.text = lifeBehaviour.CurrentHealth.ToString();
        setLight(soul.GetComponent<Light>().intensity);
        setHealth(lifeBehaviour.CurrentHealth);
    }

    public bool PrintUIMessage(string message)
    {
        bool messageShown = false;
        if (!displayingMessage)
        {
            UIMessageBG = transform.Find("UIMessage").Find("MessageBG").gameObject;
            UIMessageText = UIMessageBG.transform.Find("MessageText").gameObject;
            UIMessageText.GetComponent<Text>().text = message;

            char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
            wordCount = message.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

            StartCoroutine(Fade(wordCount));
            messageShown = true;
        }
        return messageShown;
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
        //lerp doesn't reach exactly 0, so I set it manually to 0
        bgColor.a = 0f;
        textColor.a = 0f;
        UIMessageBG.GetComponent<Image>().color = bgColor;
        UIMessageText.GetComponent<Text>().color = textColor;

        displayingMessage = false;

    }

    public void PrintCheckpointMessage()
    {
        checkpointMessageBG = transform.Find("BGCheckpointMessage").gameObject;
        checkpointMessageText = checkpointMessageBG.transform.Find("CheckpointMessage").gameObject;
        StartCoroutine(CheckpointShow());
    }

    private IEnumerator CheckpointShow()
    {
        float elapsedTimeCheckpoint = 0;

        Color bgCpColor = checkpointMessageBG.GetComponent<Image>().color;
        Color textCpColor = checkpointMessageText.GetComponent<Text>().color;

        while (elapsedTimeCheckpoint < fadeDurationCheckpoint)
        {
            bgCpColor.a = Mathf.Lerp(0, .8f, elapsedTimeCheckpoint / fadeDuration);
            textCpColor.a = Mathf.Lerp(0, 1f, elapsedTimeCheckpoint / fadeDuration);
            checkpointMessageBG.GetComponent<Image>().color = bgCpColor;
            checkpointMessageText.GetComponent<Text>().color = textCpColor;

            elapsedTimeCheckpoint += Time.deltaTime;
            yield return null;
        }

        //lerp doesn't reach exactly, so I set it manually to 0
        bgCpColor.a = .8f;
        textCpColor.a = 1f;
        checkpointMessageBG.GetComponent<Image>().color = bgCpColor;
        checkpointMessageText.GetComponent<Text>().color = textCpColor;

        yield return new WaitForSeconds(checkpointDisplayTime);
        elapsedTimeCheckpoint = 0f;

        while (elapsedTimeCheckpoint < fadeDurationCheckpoint)
        {
            bgCpColor.a = Mathf.Lerp(.8f, 0f, elapsedTimeCheckpoint / fadeDurationCheckpoint);
            textCpColor.a = Mathf.Lerp(1f, 0f, elapsedTimeCheckpoint / fadeDurationCheckpoint);
            checkpointMessageBG.GetComponent<Image>().color = bgCpColor;
            checkpointMessageText.GetComponent<Text>().color = textCpColor;

            elapsedTimeCheckpoint += Time.deltaTime;
            yield return null;
        }

        //lerp doesn't reach exactly 0, so I set it manually to 0
        bgCpColor.a = 0f;
        textCpColor.a = 0f;
        checkpointMessageBG.GetComponent<Image>().color = bgCpColor;
        checkpointMessageText.GetComponent<Text>().color = textCpColor;
    }

    public void setLight(float light)
    {
        lightSlider.value = light;        
    }

    public void setHealth(int health)
    {
        healthSlider.value = health;
    }
}

