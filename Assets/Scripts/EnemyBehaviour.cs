using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private int bodyDamage;

    private bool hasDeltDamage = false;
    private bool canDropPowerUp = false;
    private bool isTutorialEnemy = false;
    private bool isBlinking = false;


    private DropManager dropManager;
    private PowerUpPoolManager powerUpPool;
    private SpriteRenderer sr;

    private void Start()
    {
        dropManager = GameObject.FindGameObjectWithTag("DropManager").GetComponent<DropManager>();
        powerUpPool = GameObject.FindGameObjectWithTag("PowerUpPool").GetComponent<PowerUpPoolManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (sr != null)
        {
            sr.color = Color.white;
        }
    }

    void Update()
    {
        if (transform.position.y >= 15 || transform.position.x >= 14 ||
            transform.position.y <= -15 || transform.position.x <= -14)
        {
            Deactivate();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (canDropPowerUp && isTutorialEnemy)
            {
                powerUpPool.GetPowerUp("SHIELD", transform.position);
            }
            else if (canDropPowerUp)
            {
                DropPowerUp();
            }

            PlayerDataManager.coins += 1;
            Deactivate();
        }
        else
        {
            if (!isBlinking)
            {
                StartCoroutine(Blink(1.0f));
            }
        }
    }

    private IEnumerator Blink(float time)
    {
        isBlinking = true;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            sr.color = Color.darkGray;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }
        sr.color = Color.white;
        isBlinking = false;
    }

    private void DropPowerUp()
    {
        if (Random.Range(1, 101) > 75)
        {
            dropManager.DropRandomPowerUp(transform.position);
        }
    }

    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
    }

    public void SetHealth()
    {
        health = maxHealth;
    }

    public void SetBodyDamage(int dmg)
    {
        bodyDamage = dmg;
    }

    public int GetBodyDamage()
    {
        return bodyDamage;
    }

    public bool HasDeltDamage()
    {
        return hasDeltDamage;
    }

    public void SetHasDeltDamage(bool value)
    {
        hasDeltDamage = value;
    }

    public void SetCanDropPowerUp(bool value)
    {
        canDropPowerUp = value;
    }

    public void SetIsTutorialEnemy()
    {
        isTutorialEnemy = true;
    }

    public bool GetIsTutorialEnemy()
    {
        return isTutorialEnemy;
    }

    private void Deactivate()
    {
        hasDeltDamage = false;
        health = maxHealth;
        gameObject.SetActive(false);
    }
}
