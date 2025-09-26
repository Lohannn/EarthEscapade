using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameObject.Find("SettingsCanvas") == null)
        {
            if (!pauseMenu.enabled)
            {
                Time.timeScale = 0.0f;
                pauseMenu.enabled = true;
                UnlockCursor();
            }
            else
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1.0f;
                LockCursor();
            }
        }
    }
}
