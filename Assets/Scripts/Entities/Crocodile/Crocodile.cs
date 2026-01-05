using Unity.Mathematics;
using UnityEngine;

public class Crocodile : Entity
{
    [Header("MVC")]
    private CRModel _crModel;
    //private CRView _crView;
    private CRController _crController;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private CrocoBullet _bulletPrefab;

    protected override void MVC()
    {
        _crModel = new CRModel(this, _rb2d, _chPackageGO.GetComponent<CRPackage>());
        _view = new CRView(_anim, this, _crModel);
        _crController = new CRController(_crModel);
    }

    protected override void Update()
    {
        _crController.FauxUpdate();
    }

    protected override void LateUpdate()
    {
        _crController.FauxLateUpdate();
        GroundDetection();
    }

    public CrocoBullet Shoot()
    {
        var crocobullet = Instantiate(_bulletPrefab, _shootPoint.position, quaternion.identity);
        if (transform.localScale.x < 0) crocobullet.transform.localScale = new Vector2(-1, 1);
        _view.Special();

        return crocobullet;
    }
}
