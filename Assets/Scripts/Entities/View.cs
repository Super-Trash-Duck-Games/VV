using UnityEngine;

public class View
{
    protected Animator _anim;
    protected Entity _entity;
    protected Model _model;
    public View(Animator anim, Entity entity, Model model)
    {
        _anim = anim;
        _entity = entity;
        _model = model;

        _model.OnMove += Move;
        _model.OnJump += Jump;
        _model.OnFall += Fall;
        _entity.OnGrounded += Grounded;
    }

    protected virtual void Move(float x)
    {
        if (x != 0)
            _anim.SetBool("Moving", true);
        else
            _anim.SetBool("Moving", false);

        if (x > 0)
            Mirror(true);
        else if (x < 0)
            Mirror(false);
    }

    protected virtual void Jump()
    {
        _anim.SetTrigger("Jump");
    }

    protected virtual void Fall()
    {
        _anim.SetTrigger("Fall");
    }

    protected virtual void Grounded(bool grounded)
    {
        _anim.SetBool("Grounded", grounded);
    }

    protected virtual void Mirror(bool mirrored)
    {
        if (mirrored)
        {
            _entity.mirrored = false;
            _entity.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            _entity.mirrored = true;
            _entity.transform.localScale = new Vector2(-1, 1);
        }
    }

    public float MorphBack()
    {
        _anim.SetTrigger("MorphBack");
        return _anim.GetCurrentAnimatorClipInfo(0).Length;
    }

    public virtual void Special()
    {

    }
}
