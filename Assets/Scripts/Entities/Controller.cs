using UnityEngine;

public  class Controller 
{
    protected Model _model;
    protected float _x;
    public Controller(Model model)
    {
        _model = model;
    }

    public virtual void FauxUpdate()
    {

    }

    public virtual void FauxLateUpdate()
    {
        Move();
        Jump();
        JumpRelease();
        Special();
        SpecialRelease();
    }

    protected virtual void Move()
    {
        _x = Input.GetAxisRaw("Horizontal");

        _model.Move(_x);
    }

    protected virtual void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            _model.Jump();
    }

    protected virtual void JumpRelease()
    {
        if (Input.GetButtonUp("Jump"))
            _model.JumpRelease();
    }

    protected virtual void Special()
    {
        if (Input.GetButtonDown("Special"))
            _model.Special();
    }

    protected virtual void SpecialRelease()
    {
        if (Input.GetButtonUp("Special"))
            _model.SpecialRelease();
    }
}
