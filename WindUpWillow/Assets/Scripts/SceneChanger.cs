using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RetryTargetScene()
    {
        SceneManager.LoadScene(PlayerMovement.lastPlayedSceneIndex);
    }
}