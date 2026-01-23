using System.Collections;
using UnityEngine;

public class ShowerWatcher : Watcher
{
    [SerializeField] private ParticleSystem _showerPS;
    private Coroutine _shower;
    [SerializeField] private AcidTank _acidTank;
    protected override void Activate()
    {
        base.Activate();

        _shower = StartCoroutine(Shower());
    }

    private IEnumerator Shower()
    {
        _showerPS.Play();
        while (_acidTank._depleted == false)
        {
            yield return null;
        }
        _showerPS.Stop();
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        if (_shower != null)
            StopCoroutine(_shower);
    }
}
