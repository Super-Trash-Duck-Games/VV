using System;
using UnityEngine;

public class Kumkum : Entity
{
    [Header("MVC")]
    private KKModel _kkModel;
    private KKView _kkView;
    private KKController _kkController;

    [Header("Slime stuff")]
    public bool crouching;

    protected override void MVC()
    {
        _kkModel = new KKModel(this, _rb2d, _mpGO.GetComponent<MovementPackage>());
        _kkView = new KKView(_anim, this, _kkModel);
        _kkController = new KKController(_kkModel);
    }

    protected override void Update()
    {
        _kkController.FauxUpdate();
    }
    protected override void LateUpdate()
    {
        _kkController.FauxLateUpdate();
    }
}