using UnityEngine;

public class Gargantuar : Entity
{
    [Header("MVC")]
    private GModel _gModel;
    private GController _gController;
    [SerializeField] private GameObject[] hitboxes;

    protected override void MVC()
    {
        _gModel = new GModel(this, _rb2d, _mpGO.GetComponent<GPackage>(), hitboxes);
        _view = new GView(_anim, this, _gModel);
        _gController = new GController(_gModel, this);
    }

    protected override void Update()
    {
        _gController.FauxUpdate();
    }
    protected override void LateUpdate()
    {
        _gController.FauxLateUpdate();
        GroundDetection();
    }
}