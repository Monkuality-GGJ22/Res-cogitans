using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlSpawnerPoints : MonoBehaviour
{
    public GameObject brawlObject;
    private BrawlController brawlController;
    private int currentNumOfEnemiesOnField;
    private int previousNumOfEnemiesOnField;
    Vector3 pos;

    void Start()
    {
        brawlController = brawlObject.GetComponent<BrawlController>();
        currentNumOfEnemiesOnField = 0;
        previousNumOfEnemiesOnField = 0;
        pos = transform.position;
    }

    void Update()
    {
        currentNumOfEnemiesOnField = gameObject.GetComponent<EnemySpawner>().enemiesNumberOnScreen;
        if(currentNumOfEnemiesOnField < previousNumOfEnemiesOnField)
        {
            brawlController.currentPoints += brawlController.pointsPerDeath * (previousNumOfEnemiesOnField - currentNumOfEnemiesOnField);
            brawlController.remainingTime += 2f;
        }
        previousNumOfEnemiesOnField = currentNumOfEnemiesOnField;

        if(gameObject.transform.name.Equals("EnemySpawner"))
            transform.position = pos + Vector3.back * Mathf.PingPong(Time.time * 10, 40);
        else
            transform.position = pos + Vector3.forward * Mathf.PingPong(Time.time * 10, 40);
    }
}
