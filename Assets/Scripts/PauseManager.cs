using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameObject.Find("SettingsCanvas") == null)
        {
            if (!pauseMenu.enabled)
            {
                Time.timeScale = 0.0f;
                pauseMenu.enabled = true;
            }
            else
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
