using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Kumkum : Entity
{
    [Header("MVC")]
    private KKModel _kkModel;
    private KKView _kkView;
    private KKController _kkController;

    private KKPackage _kKPackage;

    [Header("Slime stuff")]
    public bool crouching;
    [SerializeField] private Vector2 _wallCast;
    [SerializeField] private bool _touchingWall;
    public Action<bool> OnWalled;
    [SerializeField] private PhysicsMaterial2D _kkPM;
    [SerializeField] private ParticleSystem _stompPS;
    [SerializeField] private Collider2D _stompCollider;
    [SerializeField] private LayerMask _ceilingLM;
    public Collider2D normalCollider, slimeCollider;

    [SerializeField] private float _restartDelay;


    protected override void MVC()
    {
        _kKPackage = _chPackageGO.GetComponent<KKPackage>();
        _kkModel = new KKModel(this, _rb2d, _kKPackage, _kkPM, _stompCollider);
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

    public bool CeilingDetection()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + transform.up * _kKPackage.slimeCeilingDetectorLenght, _ceilingLM);
        if (hit.collider != null)
            return true;
        else
            return false;
    }

    public void Death()
    {
        _kkView.Death();
        _kkModel.Death();
        _rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.layer = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 0;
        }

        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(_restartDelay);
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
            Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
            Death();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 7)
            Death();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(-_wallCast.x, _wallCast.y), transform.position + new Vector3(_wallCast.x, _wallCast.y));
        if (!Application.isPlaying) return;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * _kKPackage.slimeCeilingDetectorLenght);
    }
}