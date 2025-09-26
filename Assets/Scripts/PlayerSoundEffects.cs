using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    public readonly int SHOOT = 0;
    public readonly int HURT = 1;
    public readonly int DEATH = 2;
    public readonly int SHIELDUP = 3;
    public readonly int UPGRADE = 4;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = GameManager.sfxVolume;
        }
    }

    public void PlaySoundEffect(int soundEffect)
    {
        if (audioSource == null) return;

        AudioClip clipToPlay = audioClips[soundEffect];

        audioSource.pitch = 1f;

        if (soundEffect == SHOOT)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
        }

        audioSource.PlayOneShot(clipToPlay);
    }
}