using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider sliderBrightness;

    private void Start()
    {
        if (sliderBrightness != null)
        {
            sliderBrightness.value = 1f - GameManager.brightness;

            sliderBrightness.onValueChanged.AddListener(AdjustBrightness);
        }
    }

    public void AdjustBrightness(float value)
    {
        float opacity = 1f - value;

        PersistentBrightnessManager.Instance.ApplyBrightness(opacity);
    }
}