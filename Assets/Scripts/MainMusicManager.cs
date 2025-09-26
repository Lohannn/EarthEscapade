using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusicManager : MonoBehaviour
{
    private static MainMusicManager instance;
    private AudioSource musicSource;

    private readonly KeyCode[] hesoyamCode = {
        KeyCode.H,
        KeyCode.E,
        KeyCode.S,
        KeyCode.O,
        KeyCode.Y,
        KeyCode.A,
        KeyCode.M
    };

    private int currentCodeIndex = 0;

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

        if (Input.anyKeyDown)
        { 
            KeyCode requiredKey = hesoyamCode[currentCodeIndex];

            if (Input.GetKeyDown(requiredKey))
            {
                currentCodeIndex++;

                if (currentCodeIndex == hesoyamCode.Length)
                {
                    HesoyamCodeActivated();

                    currentCodeIndex = 0;
                }
            }
            else
            {
                currentCodeIndex = 0;
            }
        }
    }

    private void HesoyamCodeActivated()
    {
        PlayerDataManager.coins += 1000;
        PlayerDataManager.stage1Cleared = true;
        PlayerDataManager.stage2Cleared = true;
        PlayerDataManager.stage3Cleared = true;
    }
}