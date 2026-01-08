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

    public override void Special()
    {
        _ep.speed = _electrifiedSpeed;
        OnElectrify?.Invoke(true);
        _ps.Play();
        _zone.SetActive(true);
    }

    public override void SpecialRelease()
    {
        _ep.speed = _oSpeed;
        OnElectrify?.Invoke(false);
        _zone.SetActive(false);
    }
}
