using UnityEngine;

public class ZModel : Model
{
    private Zap _zap;
    public ZModel(Zap entity, Rigidbody2D rb2d, EntityPackage mp) : base(entity, rb2d, mp)
    {
        _zap = entity;
        _rb2d = rb2d;
        _ep = mp;

        _zap.OnGrounded = OnGrounded;
    }
}
