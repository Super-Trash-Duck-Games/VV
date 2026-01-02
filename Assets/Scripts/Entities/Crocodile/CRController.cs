using UnityEngine;

public class CRController : Controller
{
    private CRModel _crModel;
    public CRController(CRModel model) : base(model)
    {
        _crModel = model;
    }
}
