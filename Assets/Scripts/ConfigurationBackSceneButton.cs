using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationBackSceneButton : MonoBehaviour
{
    public void Back()
    {
        UISoundEffects.Instance.ButtonClick();
        SceneManager.UnloadSceneAsync("Configurations");
    }
}
