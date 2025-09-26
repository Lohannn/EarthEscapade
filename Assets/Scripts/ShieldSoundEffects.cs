using UnityEngine;

public class ShieldSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    public readonly int HIT = 0;
    public readonly int SHIELDDOWN = 1;

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
        AudioClip clipToPlay = audioClips[soundEffect];

        AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
    }
}