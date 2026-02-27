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
        //SceneManager.sceneLoaded += TriggerAccesoryOnLevelStarted;
        StartCoroutine(SubscribeOnDelay());
    }

    private IEnumerator SubscribeOnDelay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.sceneLoaded += TriggerAccesoryOnLevelStarted;

    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator CallOnDelay()
    {
        yield return new WaitForSeconds(.1f);
        if (usingAHat)
            AccesoryChanged(current);
    }

    private void TriggerAccesoryOnLevelStarted(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CallOnDelay());
    }

    public void AccesoryChanged(Accesories current)
    {
        this.current = current;
        usingAHat = true;
        EventManager.Trigger("Accesory", current);
    }

    public void NoAccesory()
    {
        usingAHat = false;
        EventManager.Trigger("NoAccesory");
    }


}
