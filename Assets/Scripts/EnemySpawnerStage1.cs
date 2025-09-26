using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerStage1 : MonoBehaviour
{
    [SerializeField] private Canvas victoryPanel;

    [SerializeField] private bool enemyWave1Active = true;
    private bool enemyWave2Starting;
    [SerializeField] private bool enemyWave2Active;
    private bool enemyWave3Starting;
    [SerializeField] private bool enemyWave3Active;

    private bool isShieldHandled = false;

    private readonly float[] hunterSpawnPositionsX = new float[] { 2f, -4f, 4f, 0f, -2f };

    private EnemyPoolManager enemyPool;

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();
    }

    private void Update()
    {
        if (enemyWave1Active)
        {
            Wave1();
        }
        else if (enemyWave2Active)
        {
            Wave2();
        }
        else if (enemyWave3Active)
        {
            Wave3();
        }
        WavePassCheck();
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

    private IEnumerator SpawnHunterEnemiesWave3(float spawnInterval)
    {
        int currentHunterSpawn = 0;

        while (HasRangedEnemiesOnScreen())
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;

            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
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

    private void SpawnRangedEnemiesWave3(float[] spawnPositions)
    {
        float targetHeight = 0;
        float spawnHeight = 9.5f;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Vector2 position = new(0, spawnHeight);
            int speedDirection = (i % 2 == 0) ? -1 : 1;
            enemyPool.GetRangedEnemy(position, speedDirection, targetHeight, true);
            targetHeight += 1.5f;
            spawnHeight += 1.5f;
        }
    }

    private void Wave1()
    {
        enemyWave1Active = false;
        StartCoroutine(SpawnHunterEnemiesWave1(2.0f, 5));
    }

    private void Wave2()
    {
        enemyWave2Active = false;
        float[] rangedSpawnPositions = new float[] { -6f, -3f, 3f, 6f };
        SpawnRangedEnemiesWave2(rangedSpawnPositions);
        StartCoroutine(ShieldDropCheck());
    }

    private void Wave3()
    {
        enemyWave3Active = false;
        float[] rangedSpawnPositions = new float[] { 0.0f, 1.5f, 3.0f, 4.5f };

        SpawnRangedEnemiesWave3(rangedSpawnPositions);
        StartCoroutine(SpawnHunterEnemiesWave3(1.5f));

        StartCoroutine(Wave3BeatCheck());
    }

    private bool HasPowerUpsOnScreen()
    {
        return GameObject.FindGameObjectsWithTag("PowerUp").Length > 0;
    }

    private bool HasEnemiesOnScreen()
    {
        return GameObject.FindGameObjectsWithTag("EnemyTrigger").Length > 0;
    }

    private bool HasRangedEnemiesOnScreen()
    {
        return GameObject.FindGameObjectsWithTag("EnemyRanged").Length > 0;
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

    private IEnumerator ShieldDropCheck()
    {
        if (!isShieldHandled)
        {
            while (GameObject.FindGameObjectsWithTag("EnemyRanged").Length > 1)
            {
                yield return null;
            }

            if (GameObject.FindGameObjectsWithTag("EnemyRanged").Length == 1 && !GameObject.FindGameObjectsWithTag("EnemyRanged")[0].GetComponent<EnemyBehaviour>().GetIsTutorialEnemy())
            {
                EnemyBehaviour tutorialEnemy = GameObject.FindGameObjectWithTag("EnemyRanged").GetComponent<EnemyBehaviour>();
                tutorialEnemy.SetIsTutorialEnemy();
                tutorialEnemy.SetCanDropPowerUp(true);
            }

            while (HasPowerUpsOnScreen() || HasEnemiesOnScreen())
            {
                yield return null;
            }

            enemyWave3Starting = true;
            isShieldHandled = true;
        }
    }

    private IEnumerator Wave3BeatCheck()
    {
        while (HasPowerUpsOnScreen() || HasEnemiesOnScreen())
        {
            yield return null;
        }

        StartCoroutine(OnWin());
    }

    private IEnumerator OnWin()
    {
        yield return new WaitForSeconds(3.0f);
        PlayerDataManager.stage1Cleared = true;
        victoryPanel.enabled = true;
        Time.timeScale = 0.0f;
    }
}
