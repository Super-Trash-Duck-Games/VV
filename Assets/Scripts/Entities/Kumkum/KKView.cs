using UnityEngine;

public class KKView : View
{
    private KKModel _kkModel;
    private Kumkum _kumKum;
    [SerializeField] private ParticleSystem _stompPS;

    public KKView(Animator anim, Kumkum entity, KKModel model, ParticleSystem stompPS) : base(anim, entity, model)
    {
        _anim = anim;
        _kumKum = entity;
        _kkModel = model;

        _kkModel.OnMove += Move;
        _kkModel.OnJump += Jump;
        _kkModel.OnFall += Fall;
        _kumKum.OnGrounded += Grounded;
        _kkModel.OnCrouch += Crouch;
        _kumKum.OnWalled += OnWalled;
        _kkModel.OnStomp += OnStomp;
        _kkModel.OnBite += OnBite;
        _stompPS = stompPS;
    }


    private void Crouch(bool crouching)
    {
        if (crouching)
            _anim.SetBool("Crouching", true);
        else
            _anim.SetBool("Crouching", false);
    }

    private void OnWalled(bool walled)
    {
        if (walled) _anim.SetBool("Wall", true);
        else _anim.SetBool("Wall", false);
    }

    private void OnStomp()
    {
        _anim.SetTrigger("Stomp");
        _stompPS.Play();
    }

    private void OnBite()
    {
        _anim.SetTrigger("Bite");
    }
}
