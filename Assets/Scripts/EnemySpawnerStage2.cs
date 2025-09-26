using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerStage2 : MonoBehaviour
{
    [SerializeField] private Canvas victoryPanel;

    [SerializeField] private bool enemyWave1Active = true;
    private bool enemyWave2Starting;
    [SerializeField] private bool enemyWave2Active;
    private bool enemyWave3Starting;
    [SerializeField] private bool enemyWave3Active;
    private bool enemyWave4Starting;
    [SerializeField] private bool enemyWave4Active;

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
        else if (enemyWave4Active)
        {
            Wave4();
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
            enemyPool.GetHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;

            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }
        }

        enemyWave2Starting = true;
    }

    private IEnumerator SpawnStalkerHunterEnemiesWave2(float spawnInterval, int maxHunters)
    {
        int currentHunterSpawn = 0;

        for (int currentHunters = 1; currentHunters <= maxHunters; currentHunters++)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetStalkerHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;

            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }
        }

        enemyWave3Starting = true;
    }

    private IEnumerator SpawnStalkerHunterEnemiesWave3(float spawnInterval)
    {
        int currentHunterSpawn = 0;
        while (HasRangedEnemiesOnScreen())
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetStalkerHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;
            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }
        }

        enemyWave4Starting = true;
    }

    private IEnumerator SpawnStalkerHunterEnemiesWave4(float spawnInterval)
    {
        int currentHunterSpawn = 0;
        while (HasRangedEnemiesOnScreen())
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetStalkerHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;
            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }
        }

        StartCoroutine(OnWin());
    }

    private void SpawnRangedEnemiesWave3(float[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int positionY = (i < 2) ? 7 : 5;
            Vector2 position = new(spawnPositions[i], positionY);
            int speedDirection = (i == 1 || i == 2) ? 1 : -1;
            int targetHeight = (i < 2) ? 4 : 2;

            if (i == 1 || i == 2)
            {
                enemyPool.GetShieldRangedEnemy(position, speedDirection, targetHeight);
            }
            else
            {
                enemyPool.GetRangedEnemy(position, speedDirection, targetHeight);
            }
        }
    }

    private void SpawnRangedEnemiesWave4(float[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int positionY = (i < 4) ? 7 : 6;
            Vector2 position = new(spawnPositions[i], positionY);
            int speedDirection = (i % 2 == 0) ? 1 : -1;
            print(speedDirection);
            int targetHeight = (i < 4) ? 4 : 2;

            if (i < 4)
            {
                enemyPool.GetRangedEnemy(position, speedDirection, targetHeight);
            }
            else
            {
                enemyPool.GetShieldRangedEnemy(position, speedDirection, targetHeight);
            }
        }
    }

    private void Wave1()
    {
        enemyWave1Active = false;
        StartCoroutine(SpawnHunterEnemiesWave1(1.0f, 3));
    }

    private void Wave2()
    {
        enemyWave2Active = false;
        StartCoroutine(SpawnStalkerHunterEnemiesWave2(1.0f, 7));
    }

    private void Wave3()
    {
        enemyWave3Active = false;
        float[] rangedSpawnPositions = new float[] { -4.0f, 4.0f, -4.0f, 4.0f };

        SpawnRangedEnemiesWave3(rangedSpawnPositions);
        StartCoroutine(SpawnStalkerHunterEnemiesWave3(1.0f));
    }

    private void Wave4()
    {
        enemyWave4Active = false;
        float[] rangedSpawnPositions = new float[] { -6f, -3f, 3f, 6f, -6f, -3f, 3f, 6f };
        SpawnRangedEnemiesWave4(rangedSpawnPositions);
        StartCoroutine(SpawnStalkerHunterEnemiesWave4(1.0f));
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
        yield return new WaitForSeconds(2.5f);
        enemyWave2Active = true;
    }

    private IEnumerator ToWave3Cooldown()
    {
        yield return new WaitForSeconds(4.0f);
        enemyWave3Active = true;
    }

    private IEnumerator ToWave4Cooldown()
    {
        yield return new WaitForSeconds(4.0f);
        enemyWave4Active = true;
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
        else if (!HasEnemiesOnScreen() && !HasPowerUpsOnScreen() && enemyWave4Starting)
        {
            enemyWave4Starting = false;
            StartCoroutine(ToWave4Cooldown());
        }
    }

    private IEnumerator OnWin()
    {
        yield return new WaitForSeconds(3.0f);
        PlayerDataManager.stage1Cleared = true;
        victoryPanel.enabled = true;
        Time.timeScale = 0.0f;
    }
}
