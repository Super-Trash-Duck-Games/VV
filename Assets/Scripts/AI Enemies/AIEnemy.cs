using System;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public EntityTypes entityType;
    public AIEnemiesStates currentState;

    private FiniteStateMachine _fsm;
    private Rigidbody2D _rb2d;
    private Animator _anim;
    public AIEnemyView _view { get; private set; }

    [Header("Patrol")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private GameObject _movementPackage;

    [Header("PatrolPoint")]
    [SerializeField] private float _waitTime;
    [Header("Dizzy")]
    [SerializeField] private float _dizzyTime;
    public bool isDizzy;

    [Header("Lookout parameters")]
    [SerializeField] private float _watchDistance;
    [SerializeField] private float _watchAmplitude;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector3[] _watchPoints;
    [SerializeField] private RaycastHit2D[] _casts;
    [SerializeField] private LayerMask _kkLM;
    private bool _castsSetup;
    public Action OnPlayerSeen;
    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        _rb2d = gameObject.AddComponent<Rigidbody2D>();
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        _anim = GetComponentInChildren<Animator>();
        _view = new AIEnemyView(_anim);

        EntityPackage mp = _movementPackage.GetComponent<EntityPackage>();

        _fsm = new FiniteStateMachine();

        _fsm.AddState(AIEnemiesStates.Patrol, new PatrolState(this, _waypoints, _rb2d, mp));
        _fsm.AddState(AIEnemiesStates.PatrolPoint, new PatrolPointState(this, _waitTime));
        _fsm.AddState(AIEnemiesStates.Dizzy, new DizzyState(this, _dizzyTime, _view));
        _fsm.AddState(AIEnemiesStates.Attack, new CRAttackState(this));

        _fsm.ChangeState(AIEnemiesStates.Patrol);
        _watchDistance *= -1;

        SetupWachout();
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
            _watchDistance *= -1;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            _watchDistance *= -1;
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
        _watchPoints[0] = transform.position + (Vector3)_offset;
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
                if (point.collider.gameObject.layer == 3)
                    OnPlayerSeen?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (!_castsSetup) return;
        Gizmos.DrawLine(_watchPoints[0], _watchPoints[1]);
        Gizmos.DrawLine(_watchPoints[2], _watchPoints[3]);
        Gizmos.DrawLine(_watchPoints[3], _watchPoints[1]);
        Gizmos.DrawLine(_watchPoints[3], _watchPoints[0]);
        Gizmos.DrawLine(_watchPoints[2], _watchPoints[1]);
    }
}
