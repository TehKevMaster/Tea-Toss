using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void OnStartTossinClicked()
    {
        SceneManager.LoadScene(1);
    }
}
