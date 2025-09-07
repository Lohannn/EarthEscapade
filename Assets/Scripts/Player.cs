using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] private Transform weapon;
    [SerializeField] private float reloadTime;

    private Vector2 movement;
    private Vector2 screenBounds;

    private float objectWidth;
    private float objectHeight;

    private bool onShootCooldown;

    private Rigidbody2D rb;
    private PlayerLaserPool laserPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        objectWidth = transform.GetComponent<Collider2D>().bounds.extents.x;
        objectHeight = transform.GetComponent<Collider2D>().bounds.extents.y;

        laserPool = GameObject.FindGameObjectWithTag("PlayerLaserPool").GetComponent<PlayerLaserPool>();

        onShootCooldown = false;
    }

    void Update()
    {
        PlayerInputs();
    }

    private void FixedUpdate()
    {
        OnMove();
    }
    private void LateUpdate()
    {
        LimitMovement();
    }

    private void PlayerInputs()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButton("Fire1"))
        {
            OnAttack();
        }
    }

    private void OnMove()
    {
        rb.linearVelocity = (movement).normalized * speed;
    }

    private void OnAttack()
    {
        if (Input.GetButton("Fire1") && !onShootCooldown)
        {
            laserPool.GetLaser(weapon.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }
    }

    private void LimitMovement()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    private IEnumerator Cooldown()
    {
        onShootCooldown = true;
        yield return new WaitForSeconds(reloadTime);
        onShootCooldown = false;
    }
}
