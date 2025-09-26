using UnityEngine;

public class UISoundEffects : MonoBehaviour
{
    // A única instância estática do nosso Sound Manager
    public static UISoundEffects Instance { get; private set; }

    [SerializeField] private AudioClip[] audioClips;

    public readonly int BUTTONCLICK = 0;
    public readonly int BUYITEM = 1;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlaySoundEffect(int soundEffect)
    {
        if (audioSource == null || audioClips == null || soundEffect < 0 || soundEffect >= audioClips.Length)
        {
            Debug.LogWarning("AudioSource ou AudioClip faltando para UI!");
            return;
        }

        // Garante que o volume atual do GameManager seja usado
        audioSource.volume = GameManager.sfxVolume;

        audioSource.PlayOneShot(audioClips[soundEffect]);
    }

    public void ButtonClick()
    {
        PlaySoundEffect(BUTTONCLICK);
    }

    public void BuyItem()
    {
        PlaySoundEffect(BUYITEM);
    }
}