using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyInside;
    [SerializeField]
    private float spawnTime = 1f;
    [SerializeField]
    private int maxEnemiesInside = 4;
    private int enemiesNumberOnScreen = 0;
    private float currentTime;


    private void Start()
    {
        currentTime = spawnTime;
    }

    void Update()
    {
        if (currentTime <= 0)
        {
            if (enemiesNumberOnScreen < maxEnemiesInside)
            {
                GameObject gameObject;
                gameObject = Instantiate(enemyInside, transform.position, Quaternion.identity);
                gameObject.GetComponent<SearchAndDestroy>().immaClone = true;
                enemiesNumberOnScreen++;
            }
            currentTime = spawnTime;
        }
        currentTime -= Time.deltaTime;
    }

    public void onEnemyKill()
    {
        enemiesNumberOnScreen--;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
