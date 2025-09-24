using UnityEngine;

public class StageMusicPlayer : MonoBehaviour
{
    private AudioSource music;

    private void Awake()
    {
        if (GameObject.Find("MainMusicPlayer") != null)
        {
            GameObject.Find("MainMusicPlayer").GetComponent<AudioSource>().Stop();
        }
    }

    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
        music.volume = GameManager.musicVolume;
    }
}
