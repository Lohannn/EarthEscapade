using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float speed;
    private int damage;

    private void Update()
    {
        if (transform.position.y >= 6 || transform.position.x >= 10 ||
            transform.position.y <= -6 || transform.position.x <= -10)
        {
            gameObject.SetActive(false);
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") && collision.transform.parent.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Shield>().TakeDamage(damage);
            gameObject.SetActive(false);
        } 
        else if (collision.gameObject.CompareTag("PlayerTrigger"))
        {
            collision.gameObject.GetComponentInParent<Player>().OnDamage(damage);
            gameObject.SetActive(false);
        }
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
