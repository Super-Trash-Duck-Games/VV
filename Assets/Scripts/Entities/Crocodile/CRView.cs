using UnityEngine;

public class CRView : View
{
    private CRModel _crModel;
    private Crocodile _crocodile;
    public CRView(Animator anim, Crocodile entity, CRModel model) : base(anim, entity, model)
    {
        _anim = anim;
        _crocodile = entity;
        _crModel = model;

        _crModel.OnMove += Move;
        _crModel.OnJump += Jump;
        _crModel.OnFall += Fall;
        _crocodile.OnGrounded += Grounded;
    }

    public override void Special()
    {
        _anim.SetTrigger("Shoot");
    }
}
