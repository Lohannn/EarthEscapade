using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    
    private readonly int maxHunters = 1000000;
    private int currentHunters = 0;

    private readonly float[] hunterSpawnPositionsX = new float[]{2f, -4f, 4f, 0f, -2f};
    private int currentHunterSpawn = 0;

    private readonly float[] rangedSpawnPositionsX = new float[] {-6f, -3f, 3f, 6f};

    private EnemyPoolManager enemyPool;

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();

        StartCoroutine(SpawnHunterEnemies());
        //SpawnRangedEnemies();
    }

    private void SpawnRangedEnemies()
    {
        for (int i = 0; i < rangedSpawnPositionsX.Length; i++)
        {
            Vector2 position = new(rangedSpawnPositionsX[i], 7);
            int speedDirection = (i % 2 == 0) ? -1 : 1;
            enemyPool.GetRangedEnemy(position, speedDirection);
        }
    }

    private IEnumerator SpawnHunterEnemies()
    {
        yield return new WaitForSeconds(spawnInterval);
        if (currentHunters < maxHunters)
        {
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetHunterEnemy(spawnPosition);
            currentHunterSpawn++;
            currentHunters++;

            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }

            StartCoroutine(SpawnHunterEnemies());
        }
    }
}
