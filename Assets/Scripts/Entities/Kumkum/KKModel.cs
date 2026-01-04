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

    public Action OnBite;
    private KKPackage _package;
    private Coroutine _bite;
    private BiteDetector _biteDetector;
    private AIEnemy _enemy;
    private bool _biting;
    public KKModel(Kumkum entity, Rigidbody2D rb2d, KKPackage kp, PhysicsMaterial2D kkPM, Collider2D stompCollider) : base(entity, rb2d, kp)
    {
        _kumKum = entity;
        _rb2d = rb2d;
        _package = kp;
        _kkPM = kkPM;

        _kumKum.OnGrounded += OnGrounded;
        _kumKum.OnWalled += OnWalled;
        _stompCollider = stompCollider;

        _biteDetector = _kumKum.GetComponentInChildren<BiteDetector>();
        _biteDetector.OnDetectEnemy += OnDetectEnemy;
    }

    public override void Move(float x)
    {
        if (_biting) return;
        base.Move(x);
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
            _rb2d.AddForce(_kumKum.transform.up * _package.wallJumpForce.y + Vector3.left * _package.wallJumpForce.x, ForceMode2D.Impulse);
        else
            _rb2d.AddForce(_kumKum.transform.up * _package.wallJumpForce.y + Vector3.right * _package.wallJumpForce.x, ForceMode2D.Impulse);


        OnJump?.Invoke();

        _entity.StartCoroutine(Jumping());
    }

    public override void Jump()
    {
        if (_biting) return;
        if (_crouching) return;
        base.Jump();
    }


    public void Crouch()
    {
        if (_biting) return;

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
        if (_kumKum.Grounded)
        {
            if (_bite == null)
            {
                if (!_kumKum.mirrored)
                    _bite = _kumKum.StartCoroutine(BiteRight());
                else
                    _bite = _kumKum.StartCoroutine(BiteLeft());
            }
        }
        else
            _kumKum.StartCoroutine(SpecialHang());
    }

    private IEnumerator BiteRight()
    {
        _biting = true;
        _biteDetector.TurnOn();
        OnBite?.Invoke();
        while (_kumKum.transform.localScale.x < _package.sizeGrowth - .5f)
        {
            _kumKum.transform.localScale = Vector3.Lerp(_kumKum.transform.localScale, Vector3.one * _package.sizeGrowth, _package.attackSpeed * Time.deltaTime);
            yield return null;
        }

        if (_enemy != null) _enemy.Death();

        while (_kumKum.transform.localScale.x > 1.1)
        {
            _kumKum.transform.localScale = Vector3.Lerp(_kumKum.transform.localScale, Vector3.one, _package.attackSpeed * Time.deltaTime);
            yield return null;
        }

        _kumKum.transform.localScale = Vector3.one;
        _biteDetector.TurnOff();

        _bite = null;
        _biting = false;

        MorphManager.Instance.Morph(_enemy.entityType);
        _enemy = null;
    }

    private IEnumerator BiteLeft()
    {
        _biting = true;
        _biteDetector.TurnOn();
        OnBite?.Invoke();
        while (_kumKum.transform.localScale.x > -_package.sizeGrowth + .5f)
        {
            _kumKum.transform.localScale = Vector3.Lerp(_kumKum.transform.localScale, new Vector3(-_package.sizeGrowth, _package.sizeGrowth), _package.attackSpeed * Time.deltaTime);
            yield return null;
        }

        if(_enemy != null) _enemy.Death();

        while (_kumKum.transform.localScale.x < -1.1)
        {
            _kumKum.transform.localScale = Vector3.Lerp(_kumKum.transform.localScale, new Vector3(-1, 1), _package.attackSpeed * Time.deltaTime);
            yield return null;
        }

        _kumKum.transform.localScale = new Vector3(-1, 1);
        _biteDetector.TurnOff();

        _bite = null;
        _biting = false;

        MorphManager.Instance.Morph(_enemy.entityType);
        _enemy = null;
    }

    private void OnDetectEnemy(AIEnemy enemy)
    {
        _enemy = enemy;
    }

    private IEnumerator SpecialHang()
    {
        _specialHanging = true;
        float timer = 0;
        while (_specialHanging)
        {
            timer += Time.deltaTime;

            _rb2d.linearVelocity = Vector2.zero;

            if (timer > _package.specialHangTime)
                yield break;

            yield return null;
        }

        _stompCollider.enabled = true;
        _rb2d.AddForce(_kumKum.transform.up * -_package.specialStompForce, ForceMode2D.Impulse);
        OnStomp?.Invoke();
    }

    public void SpecialRelease()
    {
        _specialHanging = false;
    }
}
