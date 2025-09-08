using UnityEngine;

public class HunterEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int bodyDamage;

    private Transform playerPosition;

    private EnemyBehaviour eb;

    private void Start()
    {
        eb = GetComponent<EnemyBehaviour>();
        eb.SetMaxHealth(maxHealth);
        eb.SetHealth();
        eb.SetBodyDamage(bodyDamage);
    }

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        LookAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y >= 10 || transform.position.x >= 14 ||
            transform.position.y <= -10 || transform.position.x <= -14)
        {
            gameObject.SetActive(false);
        }
    }

    private void LookAtPlayer()
    {
        Vector2 targetDirection = (playerPosition.position - transform.position);
        float newAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, newAngle + 90);
    }
}
