using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class KKModel : Model
{
    private Kumkum _kumKum;

    private bool _crouching;
    private bool _crouchHalt;

    public Action<bool> OnCrouch;
    private bool _walled;
    [SerializeField] private PhysicsMaterial2D _kkPM;

    private bool _specialHanging;

    public Action OnStomp;
    private Collider2D _stompCollider;

    public KKModel(Kumkum entity, Rigidbody2D rb2d, MovementPackage mp, PhysicsMaterial2D kkPM, Collider2D stompCollider) : base(entity, rb2d, mp)
    {
        _kumKum = entity;
        _rb2d = rb2d;
        _mp = mp;
        _kkPM = kkPM;

        _kumKum.OnGrounded += OnGrounded;
        _kumKum.OnWalled += OnWalled;
        _stompCollider = stompCollider;
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

        _stompCollider.enabled = false;
    }

    private void OnWalled(bool walled)
    {
        _walled = walled;

        _kumKum.StartCoroutine(Wall());
    }

    private IEnumerator Wall()
    {
        while (_rb2d.linearVelocity.y > 0) yield return null;

        _kkPM.friction = 20;

        while (_walled && !_kumKum.Grounded) yield return null;

        _kkPM.friction = .4f;
    }

    public void WallJump()
    {
        if (!_walled) return;

        if (_kumKum.transform.localScale.x > 0)
            _rb2d.AddForce(_kumKum.transform.up * _mp.wallJumpForce.y + Vector3.left * _mp.wallJumpForce.x, ForceMode2D.Impulse);
        else
            _rb2d.AddForce(_kumKum.transform.up * _mp.wallJumpForce.y + Vector3.right * _mp.wallJumpForce.x, ForceMode2D.Impulse);


        OnJump?.Invoke();

        _entity.StartCoroutine(Jumping());
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

    public override void JumpRelease()
    {
        if (_walled) return;
        base.JumpRelease();
    }


    public override void Special()
    {
        _specialHanging = true;
        _kumKum.StartCoroutine(SpecialHang());
    }

    private IEnumerator SpecialHang()
    {
        float timer = 0;
        while (_specialHanging)
        {
            timer += Time.deltaTime;

            _rb2d.linearVelocity = Vector2.zero;

            if (timer > _mp.specialHangTime)
                yield break;

            yield return null;
        }

        _stompCollider.enabled = true;
        _rb2d.AddForce(_kumKum.transform.up * -_mp.specialStompForce, ForceMode2D.Impulse);
        OnStomp?.Invoke();
    }

    public void SpecialRelease()
    {
        _specialHanging = false;
    }
}
