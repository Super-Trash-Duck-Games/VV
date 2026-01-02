using UnityEngine;

public class KKController : Controller
{
    private KKModel _kkModel;
    public KKController(KKModel model) : base(model)
    {
        _kkModel = model;
    }

    public override void FauxLateUpdate()
    {
        base.FauxLateUpdate();
        if (Input.GetButtonDown("Crouch"))
            _kkModel.Crouch();

        if (Input.GetButtonUp("Crouch"))
            _kkModel.CrouchRelease();
    }
}
