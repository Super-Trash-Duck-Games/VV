using System;
using System.Collections;
using UnityEngine;

public class Model
{
    protected Entity _entity;
    protected Rigidbody2D _rb2d;
    protected EntityPackage _ep;
    protected bool _jumping;

    public Action<float> OnMove;
    public Action OnJump;
    public Action OnFall;

    public Model(Entity entity, Rigidbody2D rb2d, EntityPackage mp)
    {
        _entity = entity;
        _rb2d = rb2d;
        _ep = mp;

        _entity.OnGrounded += OnGrounded;
    }

    public virtual void Move(float x)
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
                _rb2d.AddForce(Vector2.right * _ep.speed * 100 * x * Time.deltaTime);
            else
                _rb2d.AddForce(Vector2.right * _ep.airControl * 100 * x * Time.deltaTime);
        }
        else
            Decelerate();

            OnMove?.Invoke(x);
    }

    protected virtual void Decelerate()
    {
        _rb2d.linearVelocity = Vector2.Lerp(_rb2d.linearVelocity, new Vector2(0, _rb2d.linearVelocity.y), _ep.deceleration * Time.deltaTime);
    }

    public virtual void Jump()
    {
        if (!_entity.Grounded) return;

        _jumping = true;
        _rb2d.AddForce(Vector2.up * _ep.jumpForce, ForceMode2D.Impulse);

        OnJump?.Invoke();

        _entity.StartCoroutine(Jumping());
    }

    protected virtual IEnumerator Jumping()
    {
        yield return null;
        while (_rb2d.linearVelocityY > 0)
            yield return null;

        _rb2d.AddForce(Vector2.up * -_ep.weight, ForceMode2D.Impulse);
        _jumping = false;
        OnFall?.Invoke();
    }

    public virtual void JumpRelease()
    {
        if (!_jumping) return;

        _rb2d.AddForce(Vector2.up * -_ep.weight, ForceMode2D.Impulse);
    }

    protected virtual void OnGrounded(bool grounded)
    {

    }

    public virtual void Special()
    {

    }
}
