using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private float speed;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (transform.position.y >= 6 || transform.position.x >= 10)
        {
            gameObject.SetActive(false);
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
