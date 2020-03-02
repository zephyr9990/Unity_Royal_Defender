//Jasper Natag-oy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 20f;
    public Transform[] spawnPoints;
    public int maxEnemies = 10000;
    public int enemyCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        // to count the number of objects:
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        if (enemyCount < maxEnemies)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            enemyCount++;
        }
    }
}
