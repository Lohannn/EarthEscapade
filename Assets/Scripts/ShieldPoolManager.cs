using Unity.Mathematics;
using UnityEngine;

public class ShieldPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    private readonly GameObject[] shieldPool = new GameObject[20];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiateShields();
    }

    public GameObject GetShield(int maxHealth, float timeDuration, Transform parent) {
        foreach (var shield in shieldPool)
        {
            if (!shield.activeInHierarchy)
            {
                shield.GetComponent<Shield>().SetMaxHealth(maxHealth);
                shield.GetComponent<Shield>().SetDuration(timeDuration);
                shield.transform.SetParent(parent);
                shield.transform.localPosition = Vector2.zero;
                shield.SetActive(true);
                return shield;
            }
        }

        return CreateReserveShield(maxHealth, timeDuration, parent);
    }

    private void InstantiateShields()
    {
        for (int i = 0; i < shieldPool.Length; i++)
        {
            GameObject laser = Instantiate(shield);
            laser.SetActive(false);
            shieldPool[i] = laser;
        }
    }

    private GameObject CreateReserveShield(int maxHealth, float timeDuration, Transform parent)
    {
        GameObject reserveShield = Instantiate(shield);
        reserveShield.SetActive(false);
        reserveShield.GetComponent<Shield>().SetMaxHealth(maxHealth);
        reserveShield.GetComponent<Shield>().SetDuration(timeDuration);
        reserveShield.transform.SetParent(parent);
        reserveShield.transform.localPosition = Vector2.zero;
        reserveShield.SetActive(true);

        return reserveShield;
    }
}
