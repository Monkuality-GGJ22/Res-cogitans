using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Text soulIntensityText;
    [SerializeField] private GameObject soul;

    private GameObject UIMessageBG;
    private GameObject UIMessageText;

    private void Update()
    {
        soulIntensityText.text = soul.GetComponent<Light>().intensity.ToString("F2");
    }

    public void PrintUIMessage(string message)
    {
        UIMessageBG = transform.Find("UIMessage").Find("MessageBG").gameObject;
        UIMessageText = transform.Find("UIMessage").Find("MessageText").gameObject;
        UIMessageText.GetComponent<Text>().text = message;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Color bgColor = UIMessageBG.GetComponent<Image>().color;
        Color textColor = UIMessageText.GetComponent<Text>().color;

        for (float alpha = 0f; alpha <= 1f; alpha += Time.deltaTime)
        {
            bgColor.a = alpha;
            textColor.a = alpha;
            UIMessageBG.GetComponent<Image>().color = bgColor;
            UIMessageText.GetComponent<Text>().color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(3);

        for (float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime)
        {
            bgColor.a = alpha;
            textColor.a = alpha;
            UIMessageBG.GetComponent<Image>().color = bgColor;
            UIMessageText.GetComponent<Text>().color = textColor;
            yield return null;
        }
    }
}

