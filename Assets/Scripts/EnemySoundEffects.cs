using UnityEngine;

public class EnemySoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    public readonly int SHOOT = 0;
    public readonly int HURT = 1;
    public readonly int DEATH = 2;

    private AudioSource audioSource;

    private static int shotsPlaying = 0;
    private static readonly int maxSimultaneousShots = 4;

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

        if (soundEffect == SHOOT)
        {
            if (shotsPlaying >= maxSimultaneousShots)
            {
                return;
            }

            shotsPlaying++;
            Invoke(nameof(ResetShotCount), clipToPlay.length);

            float pitch;

            if (gameObject.CompareTag("EnemyBoss"))
            {
                pitch = 0.3f;
            }
            else
            {
                pitch = UnityEngine.Random.Range(0.85f, 1.15f);
            }

            audioSource.pitch = pitch;

            audioSource.PlayOneShot(clipToPlay);
        }
        else // HURT ou DEATH
        {
            audioSource.pitch = 1f;

            AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        }
    }

    private void ResetShotCount()
    {
        shotsPlaying = Mathf.Max(0, shotsPlaying - 1);
    }
}