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

        _fsm.ChangeState(AIEnemiesStates.Patrol);
    }

    private void Update()
    {
        OnUpdate();
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
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
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
}
