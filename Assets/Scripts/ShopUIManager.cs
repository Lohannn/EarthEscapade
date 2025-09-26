using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    private GameObject[] shopItems;

    private void Start()
    {
        shopItems = GameObject.FindGameObjectsWithTag("ShopItem");

        int stageCount = 1;
        foreach (GameObject item in shopItems)
        {
            item.SetActive(PlayerBoughtsCheck(stageCount, item.GetComponent<ShopItem>()));
            stageCount++;

            if (stageCount == 3)
            {
                stageCount = 1;
            }
        }
    }

    private bool PlayerBoughtsCheck(int stageCount, ShopItem shopItem)
    {
        string upgradeType = shopItem.GetUpgradeType();
        int stageNeeded = shopItem.GetStage();

        if (stageNeeded == 0)
        {
            if (upgradeType == "HEALTH")
            {
                if (!PlayerDataManager.firstHealthPackBought)
                {
                    return true;
                }
            }
            else if (upgradeType == "DAMAGE")
            {
                if (!PlayerDataManager.firstDamageUpgradeBought)
                {
                    return true;
                }
            }
            else if (upgradeType == "SPEED")
            {
                if (!PlayerDataManager.firstSpeedUpgradeBought)
                {
                    return true;
                }
            }
        }
        else if (stageNeeded == 1)
        {
            if (PlayerDataManager.stage1Cleared)
            {
                if (upgradeType == "HEALTH")
                {
                    if (!PlayerDataManager.secondHealthPackBought)
                    {
                        return true;
                    }
                }
                else if (upgradeType == "DAMAGE")
                {
                    if (!PlayerDataManager.secondDamageUpgradeBought)
                    {
                        return true;
                    }
                }
                else if (upgradeType == "SPEED")
                {
                    if (!PlayerDataManager.secondSpeedUpgradeBought)
                    {
                        return true;
                    }
                }
            }
        }
        else if (stageNeeded == 2)
        {
            if (PlayerDataManager.stage2Cleared)
            {
                if (upgradeType == "HEALTH")
                {
                    if (!PlayerDataManager.thirdHealthPackBought)
                    {
                        return true;
                    }
                }
                else if (upgradeType == "DAMAGE")
                {
                    if (!PlayerDataManager.thirdDamageUpgradeBought)
                    {
                        return true;
                    }
                }
                else if (upgradeType == "SPEED")
                {
                    if (!PlayerDataManager.thirdSpeedUpgradeBought)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
