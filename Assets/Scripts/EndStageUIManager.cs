using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStageUIManager : MonoBehaviour
{

    [SerializeField] private string nextStage;
    public void RetryStage()
    {
        UISoundEffects.Instance.ButtonClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextStage()
    {
        UISoundEffects.Instance.ButtonClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextStage);
    }

    public void ReturnLobby()
    {
        UISoundEffects.Instance.ButtonClick();
        Time.timeScale = 1f;
        GameObject.Find("MainMusicPlayer").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LobbyScene");
    }
}
