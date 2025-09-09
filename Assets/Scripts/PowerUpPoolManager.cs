using Unity.Mathematics;
using UnityEngine;

public class PowerUpPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private GameObject boostPrefab;
    [SerializeField] private GameObject spreadPrefab;
    [SerializeField] private GameObject shieldPrefab;

    private readonly GameObject[] healPool = new GameObject[10];
    private readonly GameObject[] boostPool = new GameObject[10];
    private readonly GameObject[] spreadPool = new GameObject[10];
    private readonly GameObject[] shieldPool = new GameObject[10];

    void Start()
    {
        InstantiatePools();
    }

    private void InstantiatePools()
    {
        InstantiateGenerics(healPool, healPrefab);
        InstantiateGenerics(boostPool, boostPrefab);
        InstantiateGenerics(spreadPool, spreadPrefab);
        InstantiateGenerics(shieldPool, shieldPrefab);
    }

    private void InstantiateGenerics(GameObject[] pool, GameObject prefab)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.SetActive(false);
            pool[i] = instance;
        }
    }

    public GameObject GetPowerUp(GameObject prefab, Vector2 position)
    {
        if (prefab == healPrefab)
        {
            return GetFromPool(healPool, healPrefab, position);
        }
        else if (prefab == boostPrefab)
        {
            return GetFromPool(boostPool, boostPrefab, position);
        }
        else if (prefab == spreadPrefab)
        {
            return GetFromPool(spreadPool, spreadPrefab, position);
        }
        else if (prefab == shieldPrefab)
        {
            return GetFromPool(shieldPool, shieldPrefab, position);
        }
        return null;
    }

    private GameObject GetFromPool(GameObject[] pool, GameObject prefab, Vector2 position)
    {
        foreach (var item in pool)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = position;
                item.SetActive(true);
                return item;
            }
        }
        return Instantiate(prefab, position, quaternion.identity);
    }
}