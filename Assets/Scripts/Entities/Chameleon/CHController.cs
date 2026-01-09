using UnityEngine;

public class CHController : Controller
{
    private CHModel _chModel;
    public CHController(CHModel model) : base(model)
    {
        _chModel = model;
    }

    protected override void Special()
    {
        base.Special();
        _chModel.AdjustGrappleLenght(Input.GetAxisRaw("Vertical"));

    }
}
