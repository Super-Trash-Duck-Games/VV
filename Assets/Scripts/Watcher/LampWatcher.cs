using System.Collections;
using UnityEngine;

public class LampWatcher : Watcher
{
    [SerializeField] private SawCannon _sawCannon;

    [SerializeField] protected float _fireRate;

    protected override void Activate()
    {
        base.Activate();
        StartCoroutine(ShootOverTime());
    }

    private IEnumerator ShootOverTime()
    {
        _sawCannon.Shoot();
        float timer = 0f;
        while (_isFollowing)
        {
            timer += Time.deltaTime;
            if (timer > _fireRate)
            {
                timer = 0;
                _sawCannon.Shoot();
            }

            yield return null;
        }
    }
}
