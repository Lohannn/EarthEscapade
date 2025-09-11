using UnityEngine;

public class HunterEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;
    private bool canDropPowerUp = false;

    private Transform playerPosition;

    private EnemyBehaviour eb;
    private Collider2D col;

    private void Start()
    {
        col = GetComponentInChildren<Collider2D>();
        eb = GetComponent<EnemyBehaviour>();
        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
        eb.SetCanDropPowerUp(canDropPowerUp);

        col.enabled = true;
    }

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        LookAtPlayer();
    }

    private void OnBecameVisible()
    {
        col.enabled = true;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public void SetCanDropPowerUp(bool value)
    {
        canDropPowerUp = value;
    }

    private void LookAtPlayer()
    {
        Vector2 targetDirection = (playerPosition.position - transform.position);
        float newAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, newAngle + 90);
    }
}
