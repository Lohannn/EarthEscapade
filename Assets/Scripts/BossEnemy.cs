using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float targetHeight;

    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    [SerializeField] private float reloadTime;

    private bool isAttacking = false;
    private bool inAlertMode = false;
    private bool canMove = true;

    private Collider2D col;
    [SerializeField] private Collider2D trig;
    private BossEnemyBehaviour eb;
    private Rigidbody2D rb;
    private LaserPoolManager laserPool;


    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        eb = gameObject.GetComponent<BossEnemyBehaviour>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();

        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
    }


    void Update()
    {
        if (canMove && !isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetHeight), 2 * Time.deltaTime);

            if (transform.position.y == targetHeight)
            {
                col.enabled = true;
                trig.enabled = true;
                canMove = false;

                leftWing.GetComponent<WingEnemy>().EnableAttack();
                rightWing.GetComponent<WingEnemy>().EnableAttack();
            }
        }

        if (isAttacking && canMove && !inAlertMode)
        {
            StartCoroutine(AttackCoroutine(reloadTime));
        }

        if (transform.position.x <= -8 || transform.position.x >= 8)
        {
            speed = -speed;
        }
    }

    private void FixedUpdate()
    {
        if (canMove && isAttacking && inAlertMode)
        {
            OnMove();
        }
    }

    private void OnMove()
    {
        rb.linearVelocity = Vector2.left * speed;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8f, 8f),
            transform.position.y,
            transform.position.z
        );
    }

    private IEnumerator AttackCoroutine(float reload)
    {
        inAlertMode = true;
        if (isAttacking)
        {
            yield return new WaitForSeconds(reload);
            laserPool.GetBossEnemyLaser(damage, weapon.position, quaternion.identity);
            StartCoroutine(AttackCoroutine(reload));
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        canMove = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }
}
