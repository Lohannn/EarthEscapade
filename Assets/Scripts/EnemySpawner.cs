using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool enemyWave1Active;
    private bool enemyWave1Completed;
    [SerializeField] private bool enemyWave2Active;
    [SerializeField] private bool enemyWave3Active;

    private readonly float[] hunterSpawnPositionsX = new float[] { 2f, -4f, 4f, 0f, -2f };

    private GameObject[] enemiesInScreen;

    private EnemyPoolManager enemyPool;
    private PowerUpPoolManager powerUpPool;

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();
        powerUpPool = GameObject.FindGameObjectWithTag("PowerUpPool").GetComponent<PowerUpPoolManager>();
    }

    private void Update()
    {
        enemiesInScreen = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyWave1Active)
        {
            Stage1Wave1();
        } 
        else if (enemyWave2Active)
        {
            enemyWave2Active = false;
            


            enemyWave3Active = true;

        }
        else if (enemyWave3Active)
        {
            enemyWave3Active = false;

        }
    }

    private void HandleEnemyHitWall(GameObject enemyThatHitTheWall, Array enemyGroups)
    {
        foreach (Array group in enemyGroups)
        {
            foreach (GameObject enemy in group)
            {
                if (enemy.gameObject == enemyThatHitTheWall)
                {
                    ChangeRangedsDirection(group);
                    return;
                }
            }
        }
    }

    private void ChangeRangedsDirection(Array enemyGroup)
    {
        foreach (GameObject ranged in enemyGroup)
        {
            ranged.GetComponent<RangedEnemy>().ChangeDirection();
        }
    }

    private IEnumerator SpawnHunterEnemies(float spawnInterval, int maxHunters)
    {
        int currentHunterSpawn = 0;

        for (int currentHunters = 0; currentHunters < maxHunters; currentHunters++)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetHunterEnemy(spawnPosition);
            currentHunterSpawn++;

            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }

            if (currentHunters >= 5)
            {
                enemyWave1Completed = true;
            }
        }
    }

    private void Stage1Wave1()
    {
        enemyWave1Active = false;
        StartCoroutine(SpawnHunterEnemies(2.0f, 5));

        if (enemiesInScreen.Length <= 0 && enemyWave1Completed)
        {
            enemyWave2Active = true;
        }
    }
}
