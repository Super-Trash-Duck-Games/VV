using UnityEngine;

public class ZController : Controller
{
    private ZModel _zModel;
    public ZController(ZModel model) : base(model)
    {
        _zModel = model;
    }
}