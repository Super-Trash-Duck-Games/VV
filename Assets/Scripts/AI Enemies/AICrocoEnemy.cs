using Unity.Mathematics;
using UnityEngine;

public class AICrocoEnemy : AIEnemy
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private CrocoBullet _bulletPrefab;

    public override void Attack()
    {
        Shoot();
    }

    public void Shoot()
    {
        var crocobullet = Instantiate(_bulletPrefab, _shootPoint.position, quaternion.identity);
        if (transform.localScale.x < 0) crocobullet.transform.localScale = new Vector2(-1, 1);
        view.Attack();
        crocobullet.TargetPlayer();
    }
}
