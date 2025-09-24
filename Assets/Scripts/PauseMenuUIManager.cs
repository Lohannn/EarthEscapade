using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUIManager : MonoBehaviour
{
    public void ResumeStage()
    {
        Time.timeScale = 1f;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void RestartStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnLobby()
    {
        Time.timeScale = 1f;
        GameObject.Find("MainMusicPlayer").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LobbyScene");
    }
}
