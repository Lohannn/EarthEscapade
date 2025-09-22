using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float brightness = 0f;
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;

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
}
