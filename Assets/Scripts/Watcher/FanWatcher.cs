using System.Collections;
using UnityEngine;

public class FanWatcher : Watcher
{
    //private Rigidbody2D _rb2d;

    [SerializeField] private float _fanTime;

    void Start()
    {
       //_rb2d = gameObject.AddComponent<Rigidbody2D>();
       //_rb2d.gravityScale = 0;
       //_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    protected override void Activate()
    {
        base.Activate();

        StartCoroutine(ActivateFan());
    }

    private IEnumerator ActivateFan()
    {
        float timer = 0;
        while (timer < _fanTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

    }

    private void Fan()
    {

    }
}
