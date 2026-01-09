using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelManager : MonoBehaviour
{
    public static ChangeLevelManager Instance;

    public string nextScene;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
