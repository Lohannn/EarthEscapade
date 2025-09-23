using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider sliderMusic;

    private void Start()
    {
        if (sliderMusic != null)
        {
            sliderMusic.value = GameManager.musicVolume;
            sliderMusic.onValueChanged.AddListener(AdjustVolume);
        }
    }

    public void AdjustVolume(float value)
    {
        GameManager.musicVolume = value;
    }
}
