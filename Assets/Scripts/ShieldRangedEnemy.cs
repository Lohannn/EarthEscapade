using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ShieldRangedEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;
    [SerializeField] private int damage;
    [SerializeField] private float targetHeight;

    [SerializeField] private Transform weapon;
    [SerializeField] private float reloadTime;

    private bool willMove;

    private bool isAttacking = false;
    private bool canChangeDirection = true;
    private bool canDropPowerUp = false;

    private Collider2D col;
    private Collider2D trig;
    private EnemyBehaviour eb;
    private Rigidbody2D rb;
    private EnemySoundEffects ese;
    private LaserPoolManager laserPool;
    private ShieldPoolManager shieldPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        col = gameObject.GetComponent<Collider2D>();
        trig = gameObject.transform.Find("RangedTrigger").GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        eb = gameObject.GetComponent<EnemyBehaviour>();
        ese = gameObject.GetComponent<EnemySoundEffects>();
        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();
        shieldPool = GameObject.FindGameObjectWithTag("ShieldPool").GetComponent<ShieldPoolManager>();

        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
        eb.SetCanDropPowerUp(canDropPowerUp);
    }

    public void Initialize(float targetHeight = 4, int speedDirection = 1, bool canDropPowerUp = false, bool movePermission = true)
    {
        col.enabled = false;
        trig.enabled = false;
        isAttacking = false;
        canChangeDirection = true;
        currentSpeed = speed;

        this.targetHeight = targetHeight;
        currentSpeed *= speedDirection;
        this.canDropPowerUp = canDropPowerUp;
        willMove = movePermission;

        shieldPool.GetShield(PowerUpManager.shieldValue, PowerUpManager.shieldDurationTime, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetHeight), 3 * Time.deltaTime);

            if (transform.position.y == targetHeight)
            {
                isAttacking = true;
                col.enabled = true;
                trig.enabled = true;
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
                ChangeDirection();

            }

            if (willMove)
            {
                OnMove();
            }
        }
    }

    private void OnMove()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        rb.linearVelocity = Vector2.left * currentSpeed;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8f, 8f),
            transform.position.y,
            transform.position.z
        );
    }

    public void SetSpeedDirection(int multiplier)
    {
        speed *= multiplier;
    }

    public void SetTargetHeight(float height)
    {
        targetHeight = height;
    }

    public void SetCanDropPowerUp(bool value)
    {
        canDropPowerUp = value;
    }

    public void ChangeDirection()
    {
        currentSpeed = -currentSpeed;
    }

    private IEnumerator AttackCoroutine(float reload)
    {
        ese.PlaySoundEffect(ese.SHOOT);
        laserPool.GetAdvancedEnemyLaser(damage, weapon.position, quaternion.identity);

        yield return new WaitForSeconds(reload);
        StartCoroutine(AttackCoroutine(reload));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canChangeDirection && collision.gameObject.CompareTag("EnemyRanged"))
        {
            ChangeDirection();
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
