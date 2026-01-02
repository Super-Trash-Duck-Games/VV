using System;
using System.Collections;
using UnityEngine;

public class KKModel : Model
{
    private Kumkum _kumKum;

    private bool _crouching;
    private bool _crouchHalt;

    public Action<bool> OnCrouch;

    public KKModel(Kumkum entity, Rigidbody2D rb2d, MovementPackage mp) : base(entity, rb2d, mp)
    {
        _kumKum = entity;
        _rb2d = rb2d;
        _mp = mp;

        _kumKum.OnGrounded += OnGrounded;
    }

    protected override void OnGrounded(bool grounded)
    {
        if (grounded)
        {
            if (_crouchHalt)
            {
                Crouch();
                _crouchHalt = false;
            }
        }
    }

    public override void Jump()
    {
        if (_crouching) return;
        base.Jump();
    }

    public void Crouch()
    {
        if (!_entity.Grounded)
        {
            _crouchHalt = true;
            return;
        }

        _crouching = true;
        OnCrouch.Invoke(_crouching);
    }

    public void CrouchRelease()
    {
        _crouching = false;
        OnCrouch?.Invoke(_crouching);
    }
}
