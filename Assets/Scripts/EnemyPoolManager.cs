using System;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject hunterEnemy;
    [SerializeField] private GameObject rangedEnemy;
    private readonly GameObject[] hunterEnemyPool = new GameObject[10];
    private readonly GameObject[] rangedEnemyPool = new GameObject[10];

    
    void Start()
    {
        InstantiateHunters();
        InstantiateRangeds();
    }

    public GameObject GetHunterEnemy(Vector2 position) {
        foreach (var enemy in hunterEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(hunterEnemy);
    }

    public GameObject GetHunterEnemy(Vector2 position, bool canDropPowerUp)
    {
        foreach (var enemy in hunterEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<HunterEnemy>().SetCanDropPowerUp(canDropPowerUp);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(hunterEnemy);
    }

    private void InstantiateHunters()
    {
        for (int i = 0; i < hunterEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(hunterEnemy);
            enemy.SetActive(false);
            hunterEnemyPool[i] = enemy;
        }
    }

    public GameObject GetRangedEnemy(Vector2 position, int speedDirection, float height)
    {
        foreach (var enemy in rangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<RangedEnemy>().Initialize(height, speedDirection);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(rangedEnemy);
    }

    public GameObject GetRangedEnemy(Vector2 position, int speedDirection, float height, bool canDropPowerUp)
    {
        foreach (var enemy in rangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<RangedEnemy>().Initialize(height, speedDirection, canDropPowerUp);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(rangedEnemy);
    }

    private void InstantiateRangeds()
    {
        for (int i = 0; i < rangedEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(rangedEnemy);
            enemy.SetActive(false);
            rangedEnemyPool[i] = enemy;
        }
    }
}
