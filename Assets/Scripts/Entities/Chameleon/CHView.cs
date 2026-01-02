using UnityEngine;

public class CHView : View
{
    private CHModel _chModel;
    private Chameleon _chameleon;
    public CHView(Animator anim, Chameleon entity, CHModel model) : base(anim, entity, model)
    {
        _anim = anim;
        _chameleon = entity;
        _chModel = model;

        _chModel.OnMove += Move;
        _chModel.OnJump += Jump;
        _chModel.OnFall += Fall;
        _chameleon.OnGrounded += Grounded;
    }
}
