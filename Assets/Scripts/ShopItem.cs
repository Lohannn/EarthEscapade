using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private int upgradeValue;
    [SerializeField] private string upgradeType;
    [SerializeField] private int stageNeeded;

    private Color showText;
    private Color hideText;

    private Text valueDisplay;

    private void Start()
    {
        valueDisplay = GameObject.FindGameObjectWithTag("ValueDisplay").GetComponent<Text>();

        showText = new Color(valueDisplay.color.r, valueDisplay.color.g, valueDisplay.color.b, 1.0f);
        hideText = new Color(valueDisplay.color.r, valueDisplay.color.g, valueDisplay.color.b, 0.0f);
    }

    public void BuyItem()
    {
        if (PlayerDataManager.coins >= value)
        {
            if (UISoundEffects.Instance != null)
            {
                UISoundEffects.Instance.BuyItem();
            }
            PlayerDataManager.coins -= value;

            switch (upgradeType) {
                case "HEALTH":
                    PlayerDataManager.maxHealth += upgradeValue;
                    break;

                case "DAMAGE":
                    PlayerDataManager.damage += upgradeValue;
                    break;

                case "SPEED":
                    PlayerDataManager.speed += upgradeValue;
                    break;
            }

            valueDisplay.color = hideText;
            PlayerDataUpdate();
            gameObject.SetActive(false);
        }
    }

    private void PlayerDataUpdate()
    {
        if (stageNeeded == 0)
        {
            if (upgradeType == "HEALTH")
            {
                PlayerDataManager.firstHealthPackBought = true;
            }
            else if (upgradeType == "DAMAGE")
            {
                PlayerDataManager.firstDamageUpgradeBought = true;
            }
            else if (upgradeType == "SPEED")
            {
                PlayerDataManager.firstSpeedUpgradeBought = true;
            }
        }
        else if (stageNeeded == 1)
        {
            if (upgradeType == "HEALTH")
            {
                PlayerDataManager.secondHealthPackBought = true;
            }
            else if (upgradeType == "DAMAGE")
            {
                PlayerDataManager.secondDamageUpgradeBought = true;
            }
            else if (upgradeType == "SPEED")
            {
                PlayerDataManager.secondSpeedUpgradeBought = true;
            }
        }
        else if (stageNeeded == 2)
        {
            if (upgradeType == "HEALTH")
            {
                PlayerDataManager.thirdHealthPackBought = true;
            }
            else if (upgradeType == "DAMAGE")
            {
                PlayerDataManager.thirdDamageUpgradeBought = true;
            }
            else if (upgradeType == "SPEED")
            {
                PlayerDataManager.thirdSpeedUpgradeBought = true;
            }
        }
    }

    public void ShowValue()
    {
        valueDisplay.text = $"-{value}";
        valueDisplay.color = showText;
    }

    public void HideValue()
    {
        valueDisplay.color = hideText;
    }

    public string GetUpgradeType() 
    { 
        return upgradeType;
    }

    public int GetStage() 
    { 
        return stageNeeded; 
    }
}
