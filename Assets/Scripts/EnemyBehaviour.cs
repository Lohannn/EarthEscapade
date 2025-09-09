using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private int bodyDamage;

    private bool hasDeltDamage = false;

    private DropManager dropManager;

    private void Start()
    {
        dropManager = GameObject.FindGameObjectWithTag("DropManager").GetComponent<DropManager>();
    }

    void Update()
    {
        if (transform.position.y >= 10 || transform.position.x >= 14 ||
            transform.position.y <= -10 || transform.position.x <= -14)
        {
            Deactivate();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (Random.Range(0, 100) > 75)
            {
                dropManager.DropRandomPowerUp(transform.position);
            }
            Deactivate();
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

    private void Deactivate()
    {
        hasDeltDamage = false;
        health = maxHealth;
        gameObject.SetActive(false);
    }
}
