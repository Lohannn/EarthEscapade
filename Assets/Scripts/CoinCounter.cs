using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = PlayerDataManager.coins.ToString();
    }
}
