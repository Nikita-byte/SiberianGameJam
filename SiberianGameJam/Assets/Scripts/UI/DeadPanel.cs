using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPanel : MonoBehaviour
{
    public Button ReRun;

    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
