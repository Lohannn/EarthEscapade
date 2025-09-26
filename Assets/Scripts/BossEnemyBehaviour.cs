using System.Collections;
using UnityEngine;

public class BossEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Collider2D trig;
    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    [SerializeField] private GameObject explosionPrefab;

    private int maxHealth;
    private int health;
    private int bodyDamage;

    private Vector2[] explosionPositions = new Vector2[5]
    {
        new Vector2(0.68f, 1.21f),
        new Vector2(-0.91f, -1.3f),
        new Vector2(0.03f, -0.2f),
        new Vector2(-0.74f, 0.71f),
        new Vector2(0.54f, -1.0f)
    };

    private bool isBlinking = false;

    private SpriteRenderer sr;
    private EnemySoundEffects ese;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ese = GetComponent<EnemySoundEffects>();
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
            PlayerDataManager.coins += 1;
            Destruction(5);
        }
        else
        {
            ese.PlaySoundEffect(ese.HURT);
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

    public void DropHealth()
    {
        health /= 2;
    }

    private void Destruction(float animationTime)
    {
        trig.enabled = false;
        GetComponent<BossEnemy>().StopAttack();

        if (leftWing.activeInHierarchy)
        {
            leftWing.GetComponent<WingEnemyBehaviour>().Destruction(5);
        }

        if (rightWing.activeInHierarchy)
        {
            rightWing.GetComponent<WingEnemyBehaviour>().Destruction(5);
        }

        StartCoroutine(DeathBlink());
        StartCoroutine(DeathExplosions(animationTime));
    }

    private IEnumerator DeathBlink()
    {
        sr.color = Color.gray5;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DeathBlink());
    }

    private IEnumerator DeathExplosions(float animationTime)
    {
        int explosionCount = 0;
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            GameObject explosion = Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity,
                gameObject.transform
            );
            explosion.transform.localPosition = explosionPositions[explosionCount];
            ese.PlaySoundEffect(ese.HURT);
            Destroy(explosion, 0.3f);
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
            explosionCount++;

            if (explosionCount > 4)
            {
                explosionCount = 0;
            }
        }
        PlayerDataManager.coins += 15;
        Deactivate();
    }

    private void Deactivate()
    {
        ese.PlaySoundEffect(ese.DEATH);
        gameObject.SetActive(false);
    }
}
