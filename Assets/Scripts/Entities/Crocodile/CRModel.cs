using System.Collections;
using UnityEngine;

public class CRModel : Model
{
    private Crocodile _crocodile;
    private CrocoBullet _bullet;
    public CRModel(Crocodile entity, Rigidbody2D rb2d, EntityPackage mp) : base(entity, rb2d, mp)
    {
        _crocodile = entity;
        _rb2d = rb2d;
        _ep = mp;

        _crocodile.OnGrounded += OnGrounded;
    }

    public override void Special()
    {
        if (_bullet == null)
            _bullet = _crocodile.Shoot();
    }
}
