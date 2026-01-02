using UnityEngine;

public class KKView : View
{
    private KKModel _kkModel;
    private Kumkum _kumKum;
    public KKView(Animator anim, Kumkum entity, KKModel model) : base(anim, entity, model)
    {
        _anim = anim;
        _kumKum = entity;
        _kkModel = model;

        _kkModel.OnMove += Move;
        _kkModel.OnJump += Jump;
        _kkModel.OnFall += Fall;
        _kumKum.OnGrounded += Grounded;
        _kkModel.OnCrouch += Crouch;
    }


    private void Crouch(bool crouching)
    {
        if (crouching)
            _anim.SetBool("Crouching", true);
        else
            _anim.SetBool("Crouching", false);
    }
}
