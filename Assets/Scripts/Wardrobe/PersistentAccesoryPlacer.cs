using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAccesoryPlacer : MonoBehaviour
{
    public static PersistentAccesoryPlacer Instance;
    public bool usingAHat;
    public Accesories current;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        SceneManager.sceneLoaded += TriggerAccesoryOnLevelStarted;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator CallOnDelay()
    {
        yield return new WaitForSeconds(.1f);
        AccesoryChanged(current);
    }

    private void TriggerAccesoryOnLevelStarted(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CallOnDelay());
    }

    public void AccesoryChanged(Accesories current)
    {
        this.current = current;
        EventManager.Trigger("Accesory", current);
    }


}
