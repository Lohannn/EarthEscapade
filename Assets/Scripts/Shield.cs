using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private float durationTime;
    private bool haveDuration = false;

    private IInvincible parentInvincibility;

    private void OnEnable()
    {
        health = maxHealth;
        parentInvincibility = GetComponentInParent<IInvincible>();
        StartCoroutine(ShieldDuration());
    }

    public void SetMaxHealth(int shieldHealth)
    {
        maxHealth = shieldHealth;
    }

    public void FixShield()
    {
        health = maxHealth;
    }

    public void SetDuration(float time)
    {
        if (time > 0)
        {
            haveDuration = true;
            durationTime = time;
        }
        else
        {
            haveDuration = false;
        }
    }

    public void ActivateShield()
    {
        parentInvincibility?.SetInvincible(true);
    }

    private void DisableShield()
    {
        parentInvincibility?.SetInvincible(false);
        health = maxHealth;
        gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DisableShield();
        }
    }

    private IEnumerator ShieldDuration()
    {
        if (haveDuration)
        {
            yield return new WaitForSeconds(durationTime);
            DisableShield();
        }
    }
}
