using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusicManager : MonoBehaviour
{
    private static MainMusicManager instance;
    private AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        musicSource.volume = GameManager.musicVolume;
    }
}