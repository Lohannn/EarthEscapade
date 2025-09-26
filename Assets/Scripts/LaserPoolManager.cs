using Unity.Mathematics;
using UnityEngine;

public class LaserPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject playerLaser;
    [SerializeField] private bool canSpawnPlayerLaser;
    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private bool canSpawnEnemyLaser;
    [SerializeField] private GameObject advancedEnemyLaser;
    [SerializeField] private bool canSpawnAdvancedEnemyLaser;
    [SerializeField] private GameObject bossEnemyLaser;
    [SerializeField] private bool canSpawnBossEnemyLaser;
    private readonly GameObject[] playerLaserPool = new GameObject[20];
    private readonly GameObject[] enemyLaserPool = new GameObject[40];
    private readonly GameObject[] advancedEnemyLaserPool = new GameObject[40];
    private readonly GameObject[] bossEnemyLaserPool = new GameObject[10];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiatePlayerLasers();
        InstantiateEnemyLasers();
        InstantiateAdvancedEnemyLasers();
        InstantiateBossEnemyLasers();
    }

    public GameObject GetPlayerLaser(int damage, Vector2 position, quaternion rotation) {
        foreach (var laser in playerLaserPool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.GetComponent<PlayerLaser>().SetDamage(damage);
                laser.transform.SetPositionAndRotation(position, rotation);
                laser.SetActive(true);
                return laser;
            }
        }

        return Instantiate(playerLaser, position, rotation);
    }

    private void InstantiatePlayerLasers()
    {
        if (!canSpawnPlayerLaser) return;

        for (int i = 0; i < playerLaserPool.Length; i++)
        {
            GameObject laser = Instantiate(playerLaser);
            laser.SetActive(false);
            playerLaserPool[i] = laser;
        }
    }

    public GameObject GetEnemyLaser(int damage, Vector2 position, quaternion rotation)
    {
        foreach (var laser in enemyLaserPool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.GetComponent<EnemyLaser>().SetDamage(damage);
                laser.transform.SetPositionAndRotation(position, rotation);
                laser.SetActive(true);
                return laser;
            }
        }

        return Instantiate(enemyLaser, position, rotation);
    }

    private void InstantiateEnemyLasers()
    {
        if (!canSpawnEnemyLaser) return;

        for (int i = 0; i < enemyLaserPool.Length; i++)
        {
            GameObject laser = Instantiate(enemyLaser);
            laser.SetActive(false);
            enemyLaserPool[i] = laser;
        }
    }

    public GameObject GetAdvancedEnemyLaser(int damage, Vector2 position, quaternion rotation)
    {
        foreach (var laser in advancedEnemyLaserPool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.GetComponent<AdvancedEnemyLaser>().SetDamage(damage);
                laser.transform.SetPositionAndRotation(position, rotation);
                laser.SetActive(true);
                return laser;
            }
        }

        return Instantiate(advancedEnemyLaser, position, rotation);
    }

    private void InstantiateAdvancedEnemyLasers()
    {
        if (!canSpawnAdvancedEnemyLaser) return;

        for (int i = 0; i < advancedEnemyLaserPool.Length; i++)
        {
            GameObject laser = Instantiate(advancedEnemyLaser);
            laser.SetActive(false);
            advancedEnemyLaserPool[i] = laser;
        }
    }

    public GameObject GetBossEnemyLaser(int damage, Vector2 position, quaternion rotation)
    {
        foreach (var laser in bossEnemyLaserPool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.GetComponent<AdvancedEnemyLaser>().SetDamage(damage);
                laser.transform.SetPositionAndRotation(position, rotation);
                laser.SetActive(true);
                return laser;
            }
        }

        return Instantiate(bossEnemyLaser, position, rotation);
    }

    private void InstantiateBossEnemyLasers()
    {
        if (!canSpawnBossEnemyLaser) return;

        for (int i = 0; i < bossEnemyLaserPool.Length; i++)
        {
            GameObject laser = Instantiate(bossEnemyLaser);
            laser.SetActive(false);
            bossEnemyLaserPool[i] = laser;
        }
    }
}
