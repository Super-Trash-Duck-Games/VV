using UnityEngine;

public class ZView : View
{
    private ZModel _zModel;
    private Zap _zap;
    public ZView(Animator anim, Zap entity, ZModel model) : base(anim, entity, model)
    {
        _anim = anim;
        _zap = entity;
        _zModel = model;

        _zModel.OnMove += Move;
        _zModel.OnJump += Jump;
        _zModel.OnFall += Fall;
        _zap.OnGrounded += Grounded;
    }
}
