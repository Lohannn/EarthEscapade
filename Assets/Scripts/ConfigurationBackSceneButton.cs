using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationBackSceneButton : MonoBehaviour
{
    public void Back()
    {
        SceneManager.UnloadSceneAsync("Configurations");
    }
}
