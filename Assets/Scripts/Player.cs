using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private int damage;
    [SerializeField] private int invincibilityTime;
    

    [SerializeField] private Transform weapon;
    [SerializeField] private float reloadTime;

    private Vector2 movement;
    private Vector2 screenBounds;

    private float objectWidth;
    private float objectHeight;

    private bool onShootCooldown;
    private bool isInvincible;

    private SpriteRenderer sr;
    private AudioSource aSource;
    private Collider2D col;
    private LaserPoolManager laserPool;
    private Collider2D playerTriggerCol;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        aSource = GetComponent<AudioSource>();
        GameObject playerTrigger = GameObject.FindWithTag("PlayerTrigger");
        playerTriggerCol = playerTrigger.GetComponent<Collider2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        objectWidth = transform.GetComponent<Collider2D>().bounds.extents.x;
        objectHeight = transform.GetComponent<Collider2D>().bounds.extents.y;

        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();

        onShootCooldown = false;
        isInvincible = false;
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            sr.color = Color.black;
            Time.timeScale = 0;
            return;
        }

        PlayerInputs();
    }

    private void LateUpdate()
    {
        LimitMovement();
    }

    private void PlayerInputs()
    {
        OnMove();

        if (Input.GetButton("Fire1"))
        {
            OnAttack();
        }
    }

    private void OnMove()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnAttack()
    {
        if (Input.GetButton("Fire1") && !onShootCooldown)
        {
            laserPool.GetPlayerLaser(damage,weapon.position, Quaternion.identity);
            //aSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            aSource.Play();
            StartCoroutine(ShootCooldown());
        }
    }

    public void OnDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            StartCoroutine(Blink(invincibilityTime));
        }
    }

    private IEnumerator Blink(float time)
    {
        isInvincible = true;
        col.enabled = false;
        playerTriggerCol.enabled = false;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        sr.enabled = true;
        col.enabled = true;
        playerTriggerCol.enabled = true;
        isInvincible = false;
    }

    private void LimitMovement()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    private IEnumerator ShootCooldown()
    {
        onShootCooldown = true;
        yield return new WaitForSeconds(reloadTime);
        onShootCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTrigger"))
        {
            OnDamage(collision.gameObject.GetComponentInParent<EnemyBehaviour>().GetBodyDamage());
        }
    }
}
