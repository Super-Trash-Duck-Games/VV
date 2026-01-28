using UnityEngine;

public class LevelProgressTracker : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    //Cada que se pasa a un nivel nuevo, guardar esa progresion con el SaveSystem

    void Start()
    {
    }

    void Update()
    {
        
    }
}
