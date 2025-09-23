using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    private int currentHealth;
    private int maxHealth;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        maxHealth = player.GetMaxHealth();
    }

    public void UpdateLifeBar()
    {
        currentHealth = player.GetHealth();

        if (maxHealth > 0)
        {
            lifeBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
