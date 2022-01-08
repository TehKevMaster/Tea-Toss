using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void OnStartTossinClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int i)
    {

        SceneManager.LoadScene(0);

    }
}
