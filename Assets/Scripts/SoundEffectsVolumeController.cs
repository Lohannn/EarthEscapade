using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsVolumeController : MonoBehaviour
{
    public Slider sliderSFX;

    private void Start()
    {
        if (sliderSFX != null)
        {
            sliderSFX.value = GameManager.sfxVolume;
            sliderSFX.onValueChanged.AddListener(AdjustVolume);
        }
    }

    public void AdjustVolume(float value)
    {
        GameManager.sfxVolume = value;
    }
}
