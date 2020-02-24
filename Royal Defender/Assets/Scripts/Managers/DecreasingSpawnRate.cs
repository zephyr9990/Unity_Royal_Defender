//Jasper Natag-oy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasingSpawnRate : MonoBehaviour
{
    public float spawnRate = 30f;
    public float spawnRateDropSpeed = 0.001f;
    public float spawnRateDeceleration = 0.001f;
    public float minSpawnRate = 1f;

    float lastSpawnTime;
    float accumulatedTime;

    void OnEnable()
    {
        accumulatedTime = 0f;
        lastSpawnTime = 0f;
    }

    void Update()
    {
        accumulatedTime += Time.deltaTime;

        spawnRateDropSpeed += spawnRateDeceleration * Time.deltaTime;
        spawnRate = Mathf.Max(spawnRate - Time.deltaTime * spawnRateDropSpeed, minSpawnRate);

        if (accumulatedTime - lastSpawnTime > spawnRate)
        {
            lastSpawnTime = accumulatedTime;
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log("Enemies will respawn faster and spawned at" + accumulatedTime);
    }
}
