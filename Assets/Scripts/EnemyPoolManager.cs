using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject hunterEnemy;
    [SerializeField] private GameObject rangedEnemy;
    private readonly GameObject[] hunterEnemyPool = new GameObject[5];
    private readonly GameObject[] rangedEnemyPool = new GameObject[4];

    
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

    private void InstantiateHunters()
    {
        for (int i = 0; i < hunterEnemyPool.Length; i++)
        {
            GameObject enemy = Instantiate(hunterEnemy);
            enemy.SetActive(false);
            hunterEnemyPool[i] = enemy;
        }
    }

    public GameObject GetRangedEnemy(Vector2 position, int speedDirection)
    {
        foreach (var enemy in rangedEnemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.GetComponent<RangedEnemy>().SetSpeedDirection(speedDirection);
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
