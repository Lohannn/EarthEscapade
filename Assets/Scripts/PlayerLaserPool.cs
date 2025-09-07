using Unity.Mathematics;
using UnityEngine;

public class PlayerLaserPool : MonoBehaviour
{
    [SerializeField] private GameObject playerLaser;
    private GameObject[] laserPool = new GameObject[20];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < laserPool.Length; i++)
        {
            GameObject laser = Instantiate(playerLaser);
            laser.SetActive(false);
            laserPool[i] = laser;
        }
    }

    public GameObject GetLaser(Vector2 position, quaternion rotation) {
        foreach (var laser in laserPool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.SetActive(true);
                laser.transform.position = position;
                laser.transform.rotation = rotation;
                return laser;
            }
        }

        return Instantiate(playerLaser, position, rotation);
    }
}
