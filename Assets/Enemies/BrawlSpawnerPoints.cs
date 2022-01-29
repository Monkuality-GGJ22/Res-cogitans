using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlSpawnerPoints : MonoBehaviour
{
    public GameObject brawlObject;
    private BrawlController brawlController;
    private int currentNumOfEnemiesOnField;
    private int previousNumOfEnemiesOnField;
    // Start is called before the first frame update
    void Start()
    {
        brawlController = brawlObject.GetComponent<BrawlController>();
        currentNumOfEnemiesOnField = 0;
        previousNumOfEnemiesOnField = 0;
}

    // Update is called once per frame
    void Update()
    {
        currentNumOfEnemiesOnField = gameObject.GetComponent<EnemySpawner>().enemiesNumberOnScreen;
        if(currentNumOfEnemiesOnField < previousNumOfEnemiesOnField)
        {
            brawlController.currentPoints += brawlController.pointsPerDeath * (previousNumOfEnemiesOnField - currentNumOfEnemiesOnField);
            brawlController.remainingTime += 2f;
        }
        previousNumOfEnemiesOnField = currentNumOfEnemiesOnField;
    }
}
