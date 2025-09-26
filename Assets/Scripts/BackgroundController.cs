using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Velocidade com que o fundo se move para baixo.")]
    public float scrollSpeed = 5f;

    [Tooltip("Arraste os seus dois Sprites de fundo para esta lista.")]
    public GameObject[] backgrounds;

    private float backgroundHeight;

    void Start()
    {
        if (backgrounds.Length > 0 && backgrounds[0].GetComponent<SpriteRenderer>() != null)
        {
            backgroundHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }

    void Update()
    {
        // Move todos os fundos para baixo.
        foreach (GameObject background in backgrounds)
        {
            background.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }

        foreach (GameObject background in backgrounds)
        {
            if (background.transform.position.y < -backgroundHeight)
            {
                float newYPosition = background.transform.position.y + (backgroundHeight * backgrounds.Length);
                background.transform.position = new Vector3(background.transform.position.x, newYPosition - 0.1f, background.transform.position.z);
            }
        }
    }
}