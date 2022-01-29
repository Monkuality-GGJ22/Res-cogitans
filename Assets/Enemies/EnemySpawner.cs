using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : RemoteActivation
{

    [SerializeField]
    private GameObject enemyInside;
    [SerializeField]
    private float spawnTime = 1f;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private int maxEnemiesInside = 4;
    public int enemiesNumberOnScreen = 0;
    private float currentTime;
    [SerializeField] private bool startDeactivated = true;


    private void Start()
    {
        currentTime = 0;
        if (startDeactivated)
            gameObject.SetActive(false);
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
                enemies.Add(gameObject);
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

    public override void Activate()
    {
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override void Respawn()
    {
        print("resoawni?");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        enemiesNumberOnScreen = 0;
        currentTime = spawnTime;
        gameObject.SetActive(false);
    }
}
