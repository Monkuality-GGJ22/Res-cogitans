using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrawlController : MonoBehaviour
{
    public float startingTime;
    public float remainingTime;
    public int currentPoints;
    public int pointsPerDeath;
    [SerializeField] private GameObject spawner0;
    [SerializeField] private GameObject spawner1;
    [SerializeField] private Canvas brawlCanvas;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject gameOverManager;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = body.GetComponent<LifeBehaviour>().deadOnce;
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        brawlCanvas.transform.Find("PointsText").GetComponent<Text>().text = "Score: "+currentPoints;
        brawlCanvas.transform.Find("TimeText").GetComponent<Text>().text = "Time:  " + (int)remainingTime;

        if (remainingTime <= 0 || isDead)
        {
            gameOverManager.GetComponent<GameOverMessage>().ShowFullscreenMessage();
        }
    }
}
