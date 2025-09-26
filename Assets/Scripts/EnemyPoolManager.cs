using System;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private bool canSpawnHunters;
    [SerializeField] private bool canSpawnStalkerHunters;
    [SerializeField] private bool canSpawnRangeds;
    [SerializeField] private bool canSpawnShieldRangeds;

    [SerializeField] private GameObject hunterEnemy;
    [SerializeField] private GameObject stalkerHunterEnemy;
    [SerializeField] private GameObject rangedEnemy;
    [SerializeField] private GameObject shieldRangedEnemy;
    private readonly GameObject[] hunterEnemyPool = new GameObject[10];
    private readonly GameObject[] stalkerHunterEnemyPool = new GameObject[10];
    private readonly GameObject[] rangedEnemyPool = new GameObject[10];
    private readonly GameObject[] shieldRangedEnemyPool = new GameObject[10];

    
    void Start()
    {
        InstantiateHunters();
        InstantiateStalkerHunters();
        InstantiateRangeds();
        InstantiateShieldRangeds();
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
        if (!canSpawnHunters) return;

        for (int i = 0; i < hunterEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(hunterEnemy);
            enemy.SetActive(false);
            hunterEnemyPool[i] = enemy;
        }
    }

    public GameObject GetStalkerHunterEnemy(Vector2 position)
    {
        foreach (var enemy in stalkerHunterEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(stalkerHunterEnemy);
    }

    public GameObject GetStalkerHunterEnemy(Vector2 position, bool canDropPowerUp)
    {
        foreach (var enemy in stalkerHunterEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<StalkerHunterEnemy>().SetCanDropPowerUp(canDropPowerUp);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(stalkerHunterEnemy);
    }

    private void InstantiateStalkerHunters()
    {
        if (!canSpawnStalkerHunters) return;

        for (int i = 0; i < stalkerHunterEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(stalkerHunterEnemy);
            enemy.SetActive(false);
            stalkerHunterEnemyPool[i] = enemy;
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

    public GameObject GetRangedEnemy(Vector2 position, int speedDirection, float height, bool canDropPowerUp, bool movePermission)
    {
        foreach (var enemy in rangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<RangedEnemy>().Initialize(height, speedDirection, canDropPowerUp, movePermission);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(rangedEnemy);
    }

    private void InstantiateRangeds()
    {
        if (!canSpawnRangeds) return;

        for (int i = 0; i < rangedEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(rangedEnemy);
            enemy.SetActive(false);
            rangedEnemyPool[i] = enemy;
        }
    }

    public GameObject GetShieldRangedEnemy(Vector2 position, int speedDirection, float height)
    {
        foreach (var enemy in shieldRangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<ShieldRangedEnemy>().Initialize(height, speedDirection);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(shieldRangedEnemy);
    }

    public GameObject GetShieldRangedEnemy(Vector2 position, int speedDirection, float height, bool canDropPowerUp)
    {
        foreach (var enemy in shieldRangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<ShieldRangedEnemy>().Initialize(height, speedDirection, canDropPowerUp);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(shieldRangedEnemy);
    }

    public GameObject GetShieldRangedEnemy(Vector2 position, int speedDirection, float height, bool canDropPowerUp, bool movePermission)
    {
        foreach (var enemy in shieldRangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<ShieldRangedEnemy>().Initialize(height, speedDirection, canDropPowerUp, movePermission);
                enemy.SetActive(true);
                return enemy;
            }
        }

        return Instantiate(rangedEnemy);
    }

    private void InstantiateShieldRangeds()
    {
        if (!canSpawnShieldRangeds) return;

        for (int i = 0; i < shieldRangedEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(shieldRangedEnemy);
            enemy.SetActive(false);
            shieldRangedEnemyPool[i] = enemy;
        }
    }
}
