using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerStage3 : MonoBehaviour
{
    [SerializeField] private GameObject bossEnemyPrefab;
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

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();
        LockCursor();
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

    private IEnumerator SpawnStalkerHunterEnemiesWave1(float spawnInterval, float time)
    {
        int currentHunterSpawn = 0;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new(hunterSpawnPositionsX[currentHunterSpawn], 7);
            enemyPool.GetStalkerHunterEnemy(spawnPosition, true);
            currentHunterSpawn++;
            elapsedTime += spawnInterval;
            if (currentHunterSpawn >= 5)
            {
                currentHunterSpawn = 0;
            }
        }

        enemyWave2Starting = true;
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

    private void SpawnRangedEnemiesWave2(float[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int positionY = (i < 5) ? 7 : 5;
            Vector2 position = new(spawnPositions[i], positionY);
            int speedDirection = (i % 2 == 0) ? 1 : -1;
            int targetHeight = (i < 5) ? 4 : 2;

            print(i % 2 != 0);
            print(i > 4);
            if (i % 2 != 0 || i > 4)
            {
                enemyPool.GetShieldRangedEnemy(position, speedDirection, targetHeight);
            }
            else
            {
                enemyPool.GetRangedEnemy(position, speedDirection, targetHeight);
            }
        }
    }

    private void SpawnRangedEnemiesWave3(float[] spawnPositions)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int positionY = (i < 4) ? 7 : 5;
            Vector2 position = new(spawnPositions[i], positionY);
            int speedDirection = (i % 2 == 0) ? 1 : -1;
            int targetHeight = (i < 4) ? 4 : 2;

            enemyPool.GetShieldRangedEnemy(position, speedDirection, targetHeight);
        }
    }

    private void Wave1()
    {
        enemyWave1Active = false;
        StartCoroutine(SpawnStalkerHunterEnemiesWave1(0.8f, 10.0f));
    }

    private void Wave2()
    {
        enemyWave2Active = false;
        float[] rangedSpawnPositions = new float[] { -7f, -3.5f, 0f, 3.5f, 7f, -3.5f, 3.5f };

        SpawnRangedEnemiesWave2(rangedSpawnPositions);
        StartCoroutine(Wave2BeatCheck());
    }

    private void Wave3()
    {
        enemyWave3Active = false;
        float[] rangedSpawnPositions = new float[] { -7f, -3f, 3f, 7f, -5f, 5f };

        SpawnRangedEnemiesWave3(rangedSpawnPositions);
        StartCoroutine(SpawnStalkerHunterEnemiesWave3(1.5f));
    }

    private void Wave4()
    {
        enemyWave4Active = false;
        Instantiate(bossEnemyPrefab, new Vector2(0, 7), Quaternion.identity);
        StartCoroutine(Wave4BeatCheck());
    }

    private IEnumerator Wave2BeatCheck()
    {
        yield return new WaitUntil(() => !HasEnemiesOnScreen() && !HasPowerUpsOnScreen());
        enemyWave3Starting = true;
    }

    private IEnumerator Wave4BeatCheck()
    {
        yield return new WaitUntil(() => !HasEnemiesOnScreen() && !HasPowerUpsOnScreen());
        StartCoroutine(OnWin());
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
        UnlockCursor();
        Time.timeScale = 0.0f;
    }
}
