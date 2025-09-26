using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private float durationTime;
    private bool haveDuration = false;
    private bool isBlinking = false;

    private IInvincible parentInvincibility;

    private SpriteRenderer sr;
    private ShieldSoundEffects sse;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sse = GetComponent<ShieldSoundEffects>();
    }

    private void OnEnable()
    {
        if (sr != null)
        {
            sr.enabled = true;
        }

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
        health += 5;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
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
            sse.PlaySoundEffect(sse.SHIELDDOWN);
            DisableShield();
        }
        else
        {
            sse.PlaySoundEffect(sse.HIT);
            if (!isBlinking)
            {
                StartCoroutine(Blink(0.5f));
            }
        }
    }
    private IEnumerator Blink(float time)
    {
        isBlinking = true;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.05f);
            elapsedTime += 0.05f;
        }
        sr.enabled = true;
        isBlinking = false;
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
