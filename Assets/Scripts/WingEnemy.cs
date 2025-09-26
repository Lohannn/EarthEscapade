using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class WingEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;
    [SerializeField] private int damage;

    [SerializeField] private Transform weapon;
    [SerializeField] private Transform externalWeapon;
    [SerializeField] private Transform hunterEjector;
    [SerializeField] private float weaponReloadTime;
    [SerializeField] private float externalWeaponReloadTime;
    [SerializeField] private float hunterEjectorReloadTime;

    private bool isAttacking = false;

    private Collider2D col;
    [SerializeField] private Collider2D trig;
    private WingEnemyBehaviour eb;
    private EnemySoundEffects ese;
    private LaserPoolManager laserPool;
    private EnemyPoolManager enemyPool;


    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        eb = gameObject.GetComponent<WingEnemyBehaviour>();
        ese = gameObject.GetComponent<EnemySoundEffects>();
        laserPool = GameObject.FindGameObjectWithTag("LaserPool").GetComponent<LaserPoolManager>();
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();

        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
    }

    private IEnumerator WeaponCoroutine(float reload)
    {
        if (!isAttacking) yield break;

        yield return new WaitForSeconds(reload);
        ese.PlaySoundEffect(ese.SHOOT);
        laserPool.GetEnemyLaser(damage, weapon.position, quaternion.identity);
        StartCoroutine(WeaponCoroutine(reload));
    }

    private IEnumerator ExternalWeaponCoroutine(float reload)
    {
        if (!isAttacking) yield break;

        yield return new WaitForSeconds(reload);
        ese.PlaySoundEffect(ese.SHOOT);
        laserPool.GetAdvancedEnemyLaser(damage, externalWeapon.position, quaternion.identity);
        StartCoroutine(ExternalWeaponCoroutine(reload));
    }

    private IEnumerator HunterEjectorCoroutine(float reload)
    {
        if (!isAttacking) yield break;

        yield return new WaitForSeconds(reload);
        enemyPool.GetHunterEnemy(hunterEjector.transform.position, true);
        StartCoroutine(HunterEjectorCoroutine(reload));
    }

    public void EnableAttack()
    {
        trig.enabled = true;
        isAttacking = true;
        StartCoroutine(WeaponCoroutine(weaponReloadTime));
        StartCoroutine(ExternalWeaponCoroutine(externalWeaponReloadTime));
        StartCoroutine(HunterEjectorCoroutine(hunterEjectorReloadTime));
    }

    public void DisableAttack()
    {
        trig.enabled = false;
        isAttacking = false;
    }
}
