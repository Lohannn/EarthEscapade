using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour
{
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

    public void ResumeStage()
    {
        Time.timeScale = 1f;
        UISoundEffects.Instance.ButtonClick();
        LockCursor();
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void RestartStage()
    {
        Time.timeScale = 1f;
        UISoundEffects.Instance.ButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnLobby()
    {
        Time.timeScale = 1f;
        UISoundEffects.Instance.ButtonClick();
        GameObject.Find("MainMusicPlayer").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LobbyScene");
    }
}
