using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IInvincible
{
    [SerializeField] private float baseSpeed;
    private float currentSpeed;
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private int damage;
    [SerializeField] private float invincibilityTime;
    

    [SerializeField] private Transform weapon;
    [SerializeField] private float baseReloadTime;
    private float currentReloadTime;
    [SerializeField] private float spreadShotAngle;

    private Vector2 movement;
    private Vector2 screenBounds;

    private float objectWidth;
    private float objectHeight;

    private bool onShootCooldown;
    private bool onSpreadShot;
    private bool isInvincible;

    private Coroutine currentPowerUpCoroutine;

    private SpriteRenderer sr;
    private AudioSource aSource;
    private Collider2D col;
    private LaserPoolManager laserPool;
    private ShieldPoolManager shieldPool;
    private Collider2D playerTriggerCol;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        aSource = GetComponent<AudioSource>();
        GameObject playerTrigger = GameObject.FindWithTag("PlayerTrigger");
        playerTriggerCol = playerTrigger.GetComponent<Collider2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        objectWidth = transform.GetComponent<Collider2D>().bounds.extents.x;
        objectHeight = transform.GetComponent<Collider2D>().bounds.extents.y;

        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();
        shieldPool = GameObject.FindGameObjectWithTag("ShieldPool").GetComponent<ShieldPoolManager>();

        currentSpeed = baseSpeed;
        currentReloadTime = baseReloadTime;

        onShootCooldown = false;
        onSpreadShot = false;
        isInvincible = false;
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            sr.color = Color.black;
            StartCoroutine(OnDeath());
            return;
        }

        PlayerInputs();

        aSource.volume = GameManager.Instance.sfxVolume;
    }

    private void LateUpdate()
    {
        LimitMovement();
    }

    private void PlayerInputs()
    {
        OnMove();

        if (Input.GetButton("Fire1") && !onShootCooldown)
        {
            OnAttack();
        }
    }

    private void OnMove()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement * currentSpeed * Time.deltaTime);
    }

    private void OnAttack()
    {
        if (onSpreadShot)
        {
            laserPool.GetPlayerLaser(damage, weapon.position, Quaternion.Euler(0, 0, -spreadShotAngle));
            laserPool.GetPlayerLaser(damage, weapon.position, Quaternion.identity);
            laserPool.GetPlayerLaser(damage, weapon.position, Quaternion.Euler(0, 0, spreadShotAngle));
        }
        else
        {
            laserPool.GetPlayerLaser(damage, weapon.position, Quaternion.identity);
        }

        aSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        aSource.Play();
        StartCoroutine(ShootCooldown(currentReloadTime));
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    private void OnGetHit(int damageTaken)
    {
        Shield shield = GetComponentInChildren<Shield>();
        if (shield != null)
        {
            shield.TakeDamage(damageTaken);
        }
        else
        {
            OnDamage(damageTaken);
        }
    }

    public void OnDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            StartCoroutine(Blink(invincibilityTime));
        }
    }

    private IEnumerator Blink(float time)
    {
        SetInvincible(true);
        playerTriggerCol.enabled = false;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        sr.enabled = true;
        playerTriggerCol.enabled = true;
        SetInvincible(false);
    }

    public void OnHeal(int healValue)
    {
        health += healValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public bool IsShielded()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Shield"))
            {
                return true;
            }
        }

        return false;
    }

    private void LimitMovement()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    private IEnumerator ShootCooldown(float reloadTime)
    {
        onShootCooldown = true;
        yield return new WaitForSeconds(reloadTime);
        onShootCooldown = false;
    }

    private IEnumerator SpreadShotMode(float durationTime)
    {
        onSpreadShot = true;
        yield return new WaitForSeconds(durationTime);
        onSpreadShot = false;

        currentPowerUpCoroutine = null;
    }

    private IEnumerator BoostMode(float durationTime, float speedMultiplier, float reloadDivider)
    {
        currentSpeed = baseSpeed * speedMultiplier;
        currentReloadTime = baseReloadTime * reloadDivider;
        yield return new WaitForSeconds(durationTime);
        currentSpeed = baseSpeed;
        currentReloadTime = baseReloadTime;

        currentPowerUpCoroutine = null;
    }

    private IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScene");
    }

    private void TurnOffBoosters()
    {
        if (currentPowerUpCoroutine != null)
        {
            StopCoroutine(currentPowerUpCoroutine);
            currentSpeed = baseSpeed;
            currentReloadTime = baseReloadTime;
            currentPowerUpCoroutine = null;
            onSpreadShot = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemyTrigger":
                EnemyBehaviour enemy = collision.gameObject.GetComponentInParent<EnemyBehaviour>();

                if (enemy != null)
                {
                    if (enemy.gameObject.CompareTag("EnemyHunter"))
                    {
                        if (!enemy.HasDeltDamage())
                        {
                            OnGetHit(enemy.GetBodyDamage());
                            enemy.SetHasDeltDamage(true);
                        }
                    }
                    else
                    {
                        OnGetHit(enemy.GetBodyDamage());
                    }
                }
                break;

            case "PowerUpHeal":
                OnHeal(collision.gameObject.GetComponent<PowerUp>().GetHealValue());
                collision.gameObject.SetActive(false);
                break;

            case "PowerUpBoost":
                TurnOffBoosters();
                float boostDurationTime = collision.gameObject.GetComponent<PowerUp>().GetBoostDurationTime();
                float boostSpeedMultiplier = collision.gameObject.GetComponent<PowerUp>().GetBoostSpeedMultiplier();
                float boostReloadDivider = collision.gameObject.GetComponent<PowerUp>().GetBoostReloadDivider();
                currentPowerUpCoroutine = StartCoroutine(BoostMode(boostDurationTime, boostSpeedMultiplier, boostReloadDivider));
                collision.gameObject.SetActive(false);
                break;

            case "PowerUpSpread":
                TurnOffBoosters();
                float spreadDurationTime = collision.gameObject.GetComponent<PowerUp>().GetSpreadDurationTime();
                currentPowerUpCoroutine = StartCoroutine(SpreadShotMode(spreadDurationTime));
                collision.gameObject.SetActive(false);
                break;

            case "PowerUpShield":
                if (!IsShielded())
                {
                    int shieldHealth = collision.gameObject.GetComponent<PowerUp>().GetShieldHealth();
                    float shieldDurationTime = collision.gameObject.GetComponent<PowerUp>().GetShieldDurationTime();
                    GameObject pickedShield = shieldPool.GetShield(shieldHealth, shieldDurationTime, transform);
                    pickedShield.GetComponent<Shield>().ActivateShield();
                }
                else
                {
                    GetComponentInChildren<Shield>().FixShield();
                }
                collision.gameObject.SetActive(false);
                break;
        }
    }
}
