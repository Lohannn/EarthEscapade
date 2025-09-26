using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectionUIManager : MonoBehaviour
{
    [SerializeField] private Canvas stage1NotCleared;
    [SerializeField] private Canvas stage2NotCleared;

    public void Stage1()
    {
        UISoundEffects.Instance.ButtonClick();
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2()
    {
        UISoundEffects.Instance.ButtonClick();
        if (PlayerDataManager.stage1Cleared)
        {
            SceneManager.LoadScene("Stage2");
        }
        else
        {
            StartCoroutine(Stage1NotCleared());
        }

    }

    public void Stage3()
    {
        UISoundEffects.Instance.ButtonClick();
        if (PlayerDataManager.stage2Cleared)
        {
            SceneManager.LoadScene("Stage3");
        }
        else
        {
            StartCoroutine(Stage2NotCleared());
        }
    }

    private IEnumerator Stage1NotCleared()
    {
        stage1NotCleared.enabled = true;
        yield return new WaitForSeconds(2f);
        stage1NotCleared.enabled = false;
    }

    private IEnumerator Stage2NotCleared()
    {
        stage2NotCleared.enabled = true;
        yield return new WaitForSeconds(2f);
        stage2NotCleared.enabled = false;
    }
}
