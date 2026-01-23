using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class DroneWatcher : Watcher
{
    [SerializeField] private float _speed;
    [SerializeField] private float _tolerance;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private ParticleSystem _bulletPS;

    void Start()
    {
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        int index = 0;
        while (true)
        {
            if (Vector2.Distance(transform.position, _waypoints[index].position) > _tolerance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _waypoints[index].position, _speed * Time.deltaTime);
                yield return null;
            }
            else
            {
                if (index == 0) index = 1;
                else index = 0;
            }
            yield return null;
        }
    }

    protected override void Activate()
    {
        base.Activate();
        _bulletPS.Play();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        _bulletPS.Stop();
    }
}
