using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private int bodyDamage;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = maxHealth;
            gameObject.SetActive(false);
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
}
