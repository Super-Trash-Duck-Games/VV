using System;
using UnityEngine;

public class ZModel : Model
{
    private Zap _zap;
    public Action<bool> OnElectrify;
    private ParticleSystem _ps;
    private GameObject _zone;
    private float _electrifiedSpeed;
    private float _oSpeed;
    public ZModel(Zap entity, Rigidbody2D rb2d, EntityPackage mp, ParticleSystem ps, GameObject zone, float speed) : base(entity, rb2d, mp)
    {
        _zap = entity;
        _rb2d = rb2d;
        _ep = mp;
        _ps = ps;
        _zone = zone;
        _electrifiedSpeed = speed;

        _oSpeed = _ep.speed;

        _zap.OnGrounded = OnGrounded;
    }

    public override void Move(float x)
    {
        Vector2 moveDir;

        if (x > 0)
            moveDir = Vector2.right;
        else
            moveDir = Vector2.left;
        moveDir.y = 0;

        if (x != 0)
        {
            if (_entity.Grounded)
                _rb2d.AddForce(Vector2.right * _ep.speed * 100 * x * Time.deltaTime * _zap.electrifiedSpeed);
            else
                _rb2d.AddForce(Vector2.right * _ep.airControl * 100 * x * Time.deltaTime * _zap.electrifiedSpeed);
        }
        else
            Decelerate();

        OnMove?.Invoke(x);
    }

    public override void Special()
    {
        _ep.speed = _electrifiedSpeed;
        OnElectrify?.Invoke(true);
        _ps.Play();
        _zone.SetActive(true);
        _zap.electrified = true;
    }

    public override void SpecialRelease()
    {
        _ep.speed = _oSpeed;
        OnElectrify?.Invoke(false);
        _zone.SetActive(false);
        _zap.electrified = false;
    }
}
