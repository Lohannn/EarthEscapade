using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField]
    private DropWeights[] powerUps;
    private int totalWeight;

    private PowerUpPoolManager poolManager;

    private void Start()
    {
        poolManager = GameObject.FindGameObjectWithTag("PowerUpPool").GetComponent<PowerUpPoolManager>();

        foreach (var drop in powerUps)
        {
            totalWeight += drop.weight;
        }
    }

    public void DropRandomPowerUp(Vector3 position)
    {
        int randomNumber = Random.Range(0, totalWeight);

        foreach (var drop in powerUps)
        {
            if (randomNumber < drop.weight)
            {
                if (drop.powerUp != null)
                {
                    // Passa o prefab escolhido para o método GetPowerUp
                    poolManager.GetPowerUp(drop.powerUp, position);
                }
                return;
            }

            randomNumber -= drop.weight;
        }
    }

}
