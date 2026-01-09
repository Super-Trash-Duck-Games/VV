using UnityEngine;

public class CHModel : Model
{
    private Chameleon _chameleon;
    private CHPackage _chp;
    private bool _grappled;
    public CHModel(Chameleon entity, Rigidbody2D rb2d, CHPackage chp) : base(entity, rb2d, chp)
    {
        _chameleon = entity;
        _rb2d = rb2d;
        _chp = chp;

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
            _grappled = true;
        }
    }

    public override void SpecialRelease()
    {
        if (!_grappled) return;
        _chameleon.UnGrapple();
        _rb2d.AddForce(Vector2.up * _ep.jumpForce, ForceMode2D.Impulse);
    }

    public void AdjustGrappleLenght(float y)
    {
        if (_chameleon.currentGrapplePoint == null) return;
       if (y != 0)
           _chameleon.spring.distance += _chp.adjustDistanceRate * Time.deltaTime * -y;
    }
}
