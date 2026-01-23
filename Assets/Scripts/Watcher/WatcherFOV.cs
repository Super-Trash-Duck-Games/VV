using UnityEngine;

public class WatcherFOV : MonoBehaviour
{
    [SerializeField] private Watcher _watcher;

    void Start()
    {
        _watcher = GetComponentInParent<Watcher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _watcher.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _watcher.Exit();
        }
    }
}
