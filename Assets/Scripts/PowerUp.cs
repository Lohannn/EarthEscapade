using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private int healValue;
    private float boostSpeedMultiplier;
    private float boostReloadDivider;
    private float boostDurationTime;
    private float spreadDurationTime;
    private int shieldHealth;
    private float shieldDurationTime;

    private float speed;

    private PowerUpManager powerUpManager;

    private void Start()
    {
        powerUpManager = GameObject.Find("PowerUpEditor").GetComponent<PowerUpManager>();

        healValue = powerUpManager.healValue;
        boostSpeedMultiplier = powerUpManager.boostSpeedMultiplier;
        boostReloadDivider = powerUpManager.boostReloadDivider;
        boostDurationTime = powerUpManager.boostDurationTime;
        spreadDurationTime = powerUpManager.spreadDurationTime;
        shieldHealth = powerUpManager.shieldValue;
        shieldDurationTime = powerUpManager.shieldDurationTime;
        speed = powerUpManager.powerUpSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y >= 6 || transform.position.x >= 10 ||
            transform.position.y <= -6 || transform.position.x <= -10)
        {
            gameObject.SetActive(false);
        }
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
