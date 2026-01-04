using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Chameleon : Entity
{
    [Header("MVC")]
    private CHModel _chModel;
    //private CHView _chView;
    private CHController _chController;

    protected new CHPackage _ep;
    [Header("Grapple")]
    public List<Rigidbody2D> grapplePoints = new List<Rigidbody2D>();
    public Rigidbody2D closestGrapplePoint;
    public Rigidbody2D currentGrapplePoint;
    public SpringJoint2D spring;
    private Coroutine _closestGrappleDetection;
    [SerializeField] private CircleCollider2D _circleCol;
    [SerializeField] private LineRenderer _lr;
    private Coroutine _renderLine;
    [SerializeField] private GameObject _lrStartPoint;

    protected override void Start()
    {
        base.Start();
        spring = GetComponent<SpringJoint2D>();

        if (_circleCol == null) _circleCol = GetComponent<CircleCollider2D>();
        _circleCol.radius = _ep.grappleDetectionLenght;
        spring.enabled = false;
        if (_lr == null) _lr = GetComponent<LineRenderer>();
        _lr.enabled = false;
    }

    protected override void MVC()
    {
        _chModel = new CHModel(this, _rb2d, _mpGO.GetComponent<CHPackage>());
        _view = new CHView(_anim, this, _chModel);
        _chController = new CHController(_chModel);
    }

    protected override void Update()
    {
        _chController.FauxUpdate();
    }

    protected override void LateUpdate()
    {
        _chController.FauxLateUpdate();
        GroundDetection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GrapplePoint")
        {
            grapplePoints.Add(collision.gameObject.GetComponent<Rigidbody2D>());
            if (_closestGrappleDetection == null) _closestGrappleDetection = StartCoroutine(SelectClosestGrapplePoint());
            //closestGrapplePoint = collision.gameObject.GetComponent<Rigidbody2D>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GrapplePoint")
        {
            grapplePoints.Remove(collision.gameObject.GetComponent<Rigidbody2D>());
            if (grapplePoints.Count == 0)
                closestGrapplePoint = null;
        }
    }

    private IEnumerator SelectClosestGrapplePoint()
    {
        while (grapplePoints.Count > 0)
        {
            float dist = 1000;
            foreach (var grapplePoint in grapplePoints)
            {
                if (Vector2.Distance(transform.position, grapplePoint.position) < dist)
                {
                    dist = Vector2.Distance(transform.position, grapplePoint.position);
                    closestGrapplePoint = grapplePoint;
                }
            }
            yield return null;
        }
        _closestGrappleDetection = null;
    }

    public void Grapple()
    {
        spring.enabled = true;
        spring.connectedBody = closestGrapplePoint;
        currentGrapplePoint = closestGrapplePoint;
        _rb2d.linearDamping = _ep.rbLinearDamplingWhileGrappled;

        _renderLine = StartCoroutine(LineRender());
    }

    private IEnumerator LineRender()
    {
        float dist = Vector2.Distance(_lrStartPoint.transform.position, currentGrapplePoint.transform.position);
        float lenght = 0;
        Vector2 current = Vector2.zero;
        _lr.enabled = true;
        while (lenght < dist)
        {
            _lr.SetPosition(0, _lrStartPoint.transform.position);
            current = Vector2.Lerp(_lrStartPoint.transform.position, currentGrapplePoint.transform.position, lenght);
            _lr.SetPosition(1, current);
            lenght += Time.deltaTime * _ep.tongueExtensionSpeed;
            yield return null;
        }

        while (currentGrapplePoint != null)
        {
            _lr.SetPosition(0, _lrStartPoint.transform.position);
            _lr.SetPosition(1, currentGrapplePoint.transform.position);
            yield return null;
        }
        _lr.enabled = false;
        _renderLine = null;

    }

    public void UnGrapple()
    {
        currentGrapplePoint = null;
        spring.connectedBody = null;
        spring.enabled = false;
        _rb2d.linearDamping = 0;
        _lr.enabled = false;

        if (_renderLine != null)
        {
            StopCoroutine(_renderLine);
            _renderLine = null;
        }
    }
}
