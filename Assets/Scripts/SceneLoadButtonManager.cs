using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButtonManager : MonoBehaviour
{
    public string sceneName;

    public void OpenScene()
    {
        UISoundEffects.Instance.ButtonClick();
        if (sceneName == "Configurations")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
