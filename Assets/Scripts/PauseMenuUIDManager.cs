using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUIDManager : MonoBehaviour
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
        SceneManager.LoadScene("StarterScene");
    }
}
