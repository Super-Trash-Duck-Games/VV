using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private bool _menuOpen;
    [SerializeField] private Canvas _canvas;
    void Start()
    {
        if (_canvas == null) _canvas = GetComponent<Canvas>();
        OpenPauseCanvas(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_menuOpen)
                OpenPauseCanvas(false);
            else
                OpenPauseCanvas(true);
        }
    }

    private void OpenPauseCanvas(bool open)
    {
        _menuOpen = open;

        if (open)
        {
            Time.timeScale = 0;
            _canvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            _canvas.enabled = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _canvas.enabled = false;
    }

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
