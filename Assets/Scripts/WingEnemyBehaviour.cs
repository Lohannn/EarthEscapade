using System.Collections;
using UnityEngine;

public class WingEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject parentBoss;
    [SerializeField] private GameObject siblingWing;
    [SerializeField] private Collider2D wingTrigger;
    [SerializeField] private GameObject explosionPrefab;

    private int maxHealth;
    private int health;
    private int bodyDamage;

    private Vector2[] leftExplosionPositions = new Vector2[4]
    {
        new Vector2(1.0f, -1.13f),
        new Vector2(-1.09f, 0.46f),
        new Vector2(0.58f, 0.33f),
        new Vector2(-0.39f, -0.69f)
    };
    private Vector2[] rightExplosionPositions = new Vector2[4]
    {
        new Vector2(-1.0f, -1.13f),
        new Vector2(1.09f, 0.46f),
        new Vector2(-0.58f, 0.33f),
        new Vector2(0.39f, -0.69f)
    };
    private Vector2[] explosionsPositions;

    private bool isBlinking = false;

    private SpriteRenderer sr;
    private EnemySoundEffects ese;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ese = GetComponent<EnemySoundEffects>();

        if (gameObject.name == "leftWing")
        {
            explosionsPositions = leftExplosionPositions;
        }
        else
        {
            explosionsPositions = rightExplosionPositions;
        }
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
            Destruction(2);
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

    public void Destruction(float animationTime)
    {
        GetComponent<WingEnemy>().DisableAttack();
        sr.color = Color.darkGray;
        StartCoroutine(DeathBlink());
        StartCoroutine(DeathExplosions(animationTime));
    }

    private IEnumerator DeathBlink()
    {
        sr.enabled = !sr.enabled;
        yield return new WaitForSeconds(0.03f);
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
            explosion.transform.localPosition = explosionsPositions[explosionCount];
            ese.PlaySoundEffect(ese.HURT);
            Destroy(explosion, 0.3f);
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
            explosionCount++;

            if (explosionCount > 3)
            {
                explosionCount = 0;
            }
        }
        Deactivate();
    }

    private void Deactivate()
    {
        if (!siblingWing.activeInHierarchy)
        {
            parentBoss.GetComponent<BossEnemyBehaviour>().DropHealth();
            parentBoss.GetComponent<BossEnemy>().StartAttack();
        }

        ese.PlaySoundEffect(ese.DEATH);
        gameObject.SetActive(false);
    }
}
