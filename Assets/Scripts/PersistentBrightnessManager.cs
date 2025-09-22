using UnityEngine;
using UnityEngine.UI;

public class PersistentBrightnessManager : MonoBehaviour
{
    public static PersistentBrightnessManager Instance;

    public Image panelBrightness;

    private float brightness;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        brightness = GameManager.Instance.brightness;
        ApplyBrightness(brightness);
    }

    public void ApplyBrightness(float opacity)
    {
        Color color = panelBrightness.color;
        color.a = opacity;
        panelBrightness.color = color;

        GameManager.Instance.brightness = opacity;
    }
}
