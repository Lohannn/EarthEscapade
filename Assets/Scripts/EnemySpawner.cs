using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool enemyWave1Active;
    private bool enemyWave2Starting;
    [SerializeField] private bool enemyWave2Active;
    private bool enemyWave3Starting;
    [SerializeField] private bool enemyWave3Active;

    private readonly float[] hunterSpawnPositionsX = new float[] { 2f, -4f, 4f, 0f, -2f };

    private EnemyPoolManager enemyPool;
    private PowerUpPoolManager powerUpPool;

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();
        powerUpPool = GameObject.FindGameObjectWithTag("PowerUpPool").GetComponent<PowerUpPoolManager>();
    }

    private void Update()
    {
        if (enemyWave1Active)
        {
            Stage1Wave1();
        }
        else if (enemyWave2Active)
        {
            Stage1Wave2();
        }
        else if (enemyWave3Active)
        {
            enemyWave3Active = false;

        }

        WavePassCheck();
        ShieldDropCheck();
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

    private IEnumerator SpawnHunterEnemiesWave1(float spawnInterval, int maxHunters)
    {
        int currentHunterSpawn = 0;

        for (int currentHunters = 1; currentHunters <= maxHunters; currentHunters++)
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
                enemyWave2Starting = true;
            }
        }
    }

    private void SpawnRangedEnemiesWave2(float[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Vector2 position = new(spawnPositions[i], 7);
            int speedDirection = (i % 2 == 0) ? -1 : 1;
            enemyPool.GetRangedEnemy(position, speedDirection, 4);
        }
    }

    private void Stage1Wave1()
    {
        enemyWave1Active = false;
        StartCoroutine(SpawnHunterEnemiesWave1(2.0f, 5));
    }

    private void Stage1Wave2()
    {
        enemyWave2Active = false;
        float[] rangedSpawnPositions = new float[] { -6f, -3f, 3f, 6f };
        SpawnRangedEnemiesWave2(rangedSpawnPositions);
    }

    private bool HasPowerUpsOnScreen()
    {
        return GameObject.FindGameObjectsWithTag("PowerUp").Length > 0;
    }

    private bool HasEnemiesOnScreen()
    {
        return GameObject.FindGameObjectsWithTag("EnemyTrigger").Length > 0;
    }

    private IEnumerator ToWave2Cooldown()
    {
        yield return new WaitForSeconds(3.0f);
        enemyWave2Active = true;
    }

    private IEnumerator ToWave3Cooldown()
    {
        yield return new WaitForSeconds(3.0f);
        enemyWave3Active = true;
    }

    private void WavePassCheck()
    {
        if (!HasEnemiesOnScreen() && enemyWave2Starting)
        {
            enemyWave2Starting = false;
            StartCoroutine(ToWave2Cooldown());
        }
        else if (!HasEnemiesOnScreen() && !HasPowerUpsOnScreen() && enemyWave3Starting)
        {
            enemyWave3Starting = false;
            StartCoroutine(ToWave3Cooldown());
        }
    }

    private void ShieldDropCheck()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyRanged").Length == 1 && !GameObject.FindGameObjectsWithTag("EnemyRanged")[0].GetComponent<EnemyBehaviour>().GetIsTutorialEnemy())
        {
            EnemyBehaviour tutorialEnemy = GameObject.FindGameObjectWithTag("EnemyRanged").GetComponent<EnemyBehaviour>();
            tutorialEnemy.SetIsTutorialEnemy();
            tutorialEnemy.SetCanDropPowerUp(true);
            enemyWave3Starting = true;
        }
    }
}
