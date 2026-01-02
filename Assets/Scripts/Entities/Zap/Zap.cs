using UnityEngine;

public class Zap : Entity
{
    [Header("MVC")]
    private ZModel _zModel;
    //private ZView _zView;
    private ZController _zController;

    protected override void MVC()
    {
        _zModel = new ZModel(this, _rb2d, _mpGO.GetComponent<MovementPackage>());
        _view = new ZView(_anim, this, _zModel);
        _zController = new ZController(_zModel);
    }

    protected override void Update()
    {
        _zController.FauxUpdate();
    }

    protected override void LateUpdate()
    {
        _zController.FauxLateUpdate();
    }
}