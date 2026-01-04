using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Kumkum : Entity
{
    [Header("MVC")]
    private KKModel _kkModel;
    private KKView _kkView;
    private KKController _kkController;

    [Header("Slime stuff")]
    public bool crouching;
    [SerializeField] private Vector2 _wallCast;
    [SerializeField] private bool _touchingWall;
    public Action<bool> OnWalled;
    [SerializeField] private PhysicsMaterial2D _kkPM;
    [SerializeField] private ParticleSystem _stompPS;
    [SerializeField] private Collider2D _stompCollider;


    protected override void MVC()
    {
        _kkModel = new KKModel(this, _rb2d, _mpGO.GetComponent<MovementPackage>(), _kkPM, _stompCollider);
        _kkView = new KKView(_anim, this, _kkModel, _stompPS);
        _kkController = new KKController(_kkModel);
    }

    protected override void Update()
    {
        _kkController.FauxUpdate();
    }
    protected override void LateUpdate()
    {
        _kkController.FauxLateUpdate();
        GroundDetection();

        WallDetection();
    }

    private void WallDetection()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + new Vector3(-_wallCast.x, _wallCast.y),
                                              transform.position + new Vector3(_wallCast.x, _wallCast.y),
                                              _groundLM);

        if (hit.collider != null)
        {
            if (!_touchingWall)
            {
                _touchingWall = true;
                OnWalled?.Invoke(true);
            }
        }
        else
        {
            if (_touchingWall)
            {
                _touchingWall = false;
                OnWalled?.Invoke(false);
            }
        }
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(-_wallCast.x, _wallCast.y), transform.position + new Vector3(_wallCast.x, _wallCast.y));
    }
}