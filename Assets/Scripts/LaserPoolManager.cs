using Unity.Mathematics;
using UnityEngine;

public class LaserPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject playerLaser;
    [SerializeField] private GameObject enemyLaser;
    private readonly GameObject[] playerLaserPool = new GameObject[20];
    private readonly GameObject[] enemyLaserPool = new GameObject[40];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiatePlayerLasers();
        InstantiateEnemyLasers();
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
        for (int i = 0; i < enemyLaserPool.Length; i++)
        {
            GameObject laser = Instantiate(enemyLaser);
            laser.SetActive(false);
            enemyLaserPool[i] = laser;
        }
    }
}
