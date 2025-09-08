using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int healValue = 2;
    [SerializeField] private float boostSpeedMultiplier = 1.5f;
    [SerializeField] private float boostReloadDivider = 0.5f;
    [SerializeField] private float boostDurationTime = 5;
    [SerializeField] private float spreadDurationTime = 5;
    [SerializeField] private int shieldHealth = 10;
    [SerializeField] private float shieldDurationTime = 5;

    private void Start()
    {
        //switch (gameObject.tag)
        //{
        //    case "PowerUpHeal":
        //        break;

        //    case "PowerUpBoost":
        //        break;

        //    case "PowerUpSpread":
        //        break;

        //    case "PowerUpShield":
        //        break;
        //}
    }

    public int GetHealValue()
    {
        return healValue;
    }

    public float GetBoostSpeedMultiplier()
    {
        return boostSpeedMultiplier;
    }

    public float GetBoostReloadDivider() { 
        return boostReloadDivider;
    }

    public float GetBoostDurationTime()
    {
        return boostDurationTime;
    }

    public float GetSpreadDurationTime()
    {
        return spreadDurationTime;
    }

    public int GetShieldHealth()
    {
        return shieldHealth;
    }

    public float GetShieldDurationTime()
    {
        return shieldDurationTime;
    }
}
