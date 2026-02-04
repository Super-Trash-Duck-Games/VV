using System.Collections.Generic;
using UnityEngine;
using System;

public class AIEnemy : MonoBehaviour
{
    public EntityTypes entityType;
    public AIEnemiesStates currentState;

    protected FiniteStateMachine _fsm;
    protected Rigidbody2D _rb2d;
    protected Animator _anim;
    public AIEnemyView view { get; protected set; }

    [Header("Patrol")]
    [SerializeField] protected float _patrolAreaDistance;
    public List<Vector2> waypoints;
    [SerializeField] protected GameObject _movementPackage;

    [Header("PatrolPoint")]
    [SerializeField] protected float _waitTime;
    [Header("Dizzy")]
    [SerializeField] protected float _dizzyTime;
    public bool isDizzy;

    [Header("Lookout parameters")]
    [SerializeField] private float _watchDistance;
    private float _oWatchDistance;
    [SerializeField] private float _watchAmplitude;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector3[] _watchPoints;
    [SerializeField] private RaycastHit2D[] _casts;
    [SerializeField] protected LayerMask _kkLM;
    private bool _castsSetup;
    public Action OnPlayerSeen;
    public bool PlayerInView { get; protected set; }
    [SerializeField] protected float _cooldown = .7f;

    void Start()
    {
        _oWatchDistance = _watchDistance;
        OnStart();
    }

    protected virtual void OnStart()
    {
        if (waypoints == null)
            SetWaypoints();
        SetupComponents();
        SetupFSM();
        SetupWachout();
    }
    protected List<Vector2> SetWaypoints()
    {
        Vector2 point = Vector3.zero;

        for (int i = 0; i < 2; i++)
        {

            if (i <= 0)
            {
                point = new Vector3(transform.position.x - _patrolAreaDistance, transform.position.y, transform.position.z);
                waypoints.Add(point);
            }
            else
            {
                point = new Vector3(transform.position.x + _patrolAreaDistance, transform.position.y, transform.position.z);
                waypoints.Add(point);
            }
        }

        return waypoints;
    }

    public void SetWaypoints(Transform[] wp)
    {
        foreach (Transform t in wp)
        {
            waypoints.Add(t.position);
        }
    }

    protected virtual void SetupComponents()
    {
        _rb2d = gameObject.AddComponent<Rigidbody2D>();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        _anim = GetComponentInChildren<Animator>();
        view = new AIEnemyView(_anim);
    }

    protected virtual void SetupFSM()
    {
        _fsm = new FiniteStateMachine();

        _fsm.AddState(AIEnemiesStates.Patrol, new PatrolState(this, _rb2d, _movementPackage.GetComponent<EntityPackage>()));
        _fsm.AddState(AIEnemiesStates.PatrolPoint, new PatrolPointState(this, _waitTime));
        _fsm.AddState(AIEnemiesStates.Dizzy, new DizzyState(this, _dizzyTime, view));
        _fsm.AddState(AIEnemiesStates.Attack, new AttackState(this, _cooldown));

        _fsm.ChangeState(AIEnemiesStates.Patrol);
    }

    private void Update()
    {
        OnUpdate();
        CastWathZone();
    }

    protected virtual void OnUpdate()
    {
        _fsm.Update();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    protected virtual void OnFixedUpdate()
    {
        _fsm.FixedUpdate();
    }

    public void Mirror(bool mirrored)
    {
        if (!mirrored)
        {
            _watchDistance = _oWatchDistance;

            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            _watchDistance = -_oWatchDistance;

            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _fsm.OnTriggerEnter2D(collision);
        //if(collision.gameObject.layer == 7) Debug.Log("Fuckititty");
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void SetupWachout()
    {
        _watchPoints = new Vector3[4];
        _watchPoints[0] = transform.position + (Vector3)_offset;
        _watchPoints[1] = _watchPoints[0] + transform.right * _watchDistance;
        _watchPoints[2] = _watchPoints[0] + transform.up * _watchAmplitude;
        _watchPoints[3] = _watchPoints[0] + transform.right * _watchDistance + transform.up * _watchAmplitude;

        _casts = new RaycastHit2D[5];

        _castsSetup = true;
    }

    private void CastWathZone()
    {
        if (!_castsSetup) return;
        var mirror = 1;
        if (transform.localScale.x < 0) mirror = -1;
        _watchPoints[0] = transform.position + new Vector3(_offset.x * mirror, _offset.y);
        _watchPoints[1] = _watchPoints[0] + transform.right * _watchDistance;
        _watchPoints[2] = _watchPoints[0] + transform.up * _watchAmplitude;
        _watchPoints[3] = _watchPoints[0] + transform.right * _watchDistance + transform.up * _watchAmplitude;
        _casts[0] = Physics2D.Linecast(_watchPoints[0], _watchPoints[1], _kkLM);
        _casts[1] = Physics2D.Linecast(_watchPoints[2], _watchPoints[3], _kkLM);
        _casts[2] = Physics2D.Linecast(_watchPoints[3], _watchPoints[1], _kkLM);
        _casts[3] = Physics2D.Linecast(_watchPoints[3], _watchPoints[0], _kkLM);
        _casts[4] = Physics2D.Linecast(_watchPoints[2], _watchPoints[1], _kkLM);

        foreach (var point in _casts)
        {
            if (point.collider != null)
            {
                if (point.collider.gameObject.layer == 3)
                {
                    OnPlayerSeen?.Invoke();
                    PlayerInView = true;
                }
            }
            else
                PlayerInView = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - _patrolAreaDistance, transform.position.y, transform.position.z), .2f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + _patrolAreaDistance, transform.position.y, transform.position.z), .2f);

        Gizmos.color = Color.white;

        if (!_castsSetup) return;
        Gizmos.DrawLine(_watchPoints[0], _watchPoints[1]);
        Gizmos.DrawLine(_watchPoints[2], _watchPoints[3]);
        Gizmos.DrawLine(_watchPoints[3], _watchPoints[1]);
        Gizmos.DrawLine(_watchPoints[3], _watchPoints[0]);
        Gizmos.DrawLine(_watchPoints[2], _watchPoints[1]);
    }

    public virtual void Attack()
    {

    }
}
