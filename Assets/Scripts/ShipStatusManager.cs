using UnityEngine;
using UnityEngine.UI;

public class ShipStatusManager : MonoBehaviour
{
    [SerializeField] private Text damage;
    [SerializeField] private Text speed;
    [SerializeField] private Text maxHealth;
    [SerializeField] private Text coins;


    void Update()
    {
        damage.text = PlayerDataManager.damage.ToString();
        speed.text = PlayerDataManager.speed.ToString();
        maxHealth.text = PlayerDataManager.maxHealth.ToString();
        coins.text = PlayerDataManager.coins.ToString();
    }
}
