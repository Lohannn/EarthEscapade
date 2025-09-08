using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;
    [SerializeField] private int damage;

    [SerializeField] private Transform weapon;
    [SerializeField] private float reloadTime;

    private bool isAttacking = false;
    private bool canChangeDirection = true;

    private Collider2D col;
    private AudioSource aSource;
    private EnemyBehaviour eb;
    private Rigidbody2D rb;
    private LaserPoolManager laserPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();
        eb = GetComponent<EnemyBehaviour>();
        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();

        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 4), 3 * Time.deltaTime);

            if (transform.position.y == 4)
            {
                isAttacking = true;
                col.enabled = true;
                StartCoroutine(AttackCoroutine(reloadTime));
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {

            if (transform.position.x <= -8 || transform.position.x >= 8)
            {
                speed = -speed;
            }

            //transform.Translate(Vector2.left * speed * Time.deltaTime);
            rb.linearVelocity = Vector2.left * speed;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -8f, 8f),
                transform.position.y,
                transform.position.z
            );
        }
    }

    public void SetSpeedDirection(int multiplier)
    {
        speed *= multiplier;
    }

    private IEnumerator AttackCoroutine(float reload)
    {
        laserPool.GetEnemyLaser(damage, weapon.position, quaternion.identity);
        //aSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        aSource.Play();

        yield return new WaitForSeconds(reload);
        StartCoroutine(AttackCoroutine(reload));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canChangeDirection && collision.gameObject.CompareTag("Enemy"))
        {
            speed = -speed;
            canChangeDirection = false;
            StartCoroutine(EnableDirectionChange());
        }
    }

    private IEnumerator EnableDirectionChange()
    {
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }
}
