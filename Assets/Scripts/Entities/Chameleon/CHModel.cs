using UnityEngine;

public class CHModel : Model
{
    private Chameleon _chameleon;
    public CHModel(Chameleon entity, Rigidbody2D rb2d, MovementPackage mp) : base(entity, rb2d, mp)
    {
        _chameleon = entity;
        _rb2d = rb2d;
        _mp = mp;

        _chameleon.OnGrounded += OnGrounded;
    }

    protected override void Decelerate()
    {
        if (_chameleon.currentGrapplePoint != null) return;
        base.Decelerate();
    }

    public override void Special()
    {
        if (_chameleon.closestGrapplePoint != null)
        {
            _chameleon.Grapple();
        }
    }

    public void SpecialRelease()
    {
        _chameleon.UnGrapple();
    }

    public void AdjustGrappleLenght(float y)
    {
        if (_chameleon.currentGrapplePoint == null) return;
       if (y != 0)
           _chameleon.spring.distance += _mp.adjustDistanceRate * Time.deltaTime * -y;
    }
}
