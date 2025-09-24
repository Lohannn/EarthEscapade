using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStageUIManager : MonoBehaviour
{

    [SerializeField] private string nextStage;
    public void RetryStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextStage);
    }

    public void ReturnLobby()
    {
        Time.timeScale = 1f;
        GameObject.Find("MainMusicPlayer").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LobbyScene");
    }
}
