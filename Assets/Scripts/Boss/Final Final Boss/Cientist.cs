using System.Collections;
using UnityEngine;

public class Cientist : MonoBehaviour
{
    private FiniteStateMachine _fsm;

    [SerializeField] public CientistStates _currentState;
    [SerializeField] private CientistStates[] _stateList;
    private int _stateListCounter;

    [SerializeField] private CData _data;

    private void Start()
    {
        GetDataContents();

        _fsm = new FiniteStateMachine();

        _fsm.AddState(CientistStates.LaserGunShoot, new CLaserGunState(this, _data));
        _fsm.AddState(CientistStates.RayGunShoot, new CRayGunState(this, _data));
        _fsm.AddState(CientistStates.SpawnEnemy, new CSpawnEnemyState(this, _data));
        _fsm.AddState(CientistStates.Death, new CDeathState(this, _data));
        _fsm.AddState(CientistStates.Vulnerable, new CVulnerableState(this, _data));
        _fsm.AddState(CientistStates.Run, new CRunState(this, _data));

        _fsm.ChangeState(_stateList[0]);
    }

    private void GetDataContents()
    {
        if (_data.ff == null) _data.ff = GetComponentInChildren<CientificForceField>();
        if (_data.rayCannon == null) _data.rayCannon = FindFirstObjectByType<RayCannon>();
        if (_data.rb2d == null) _data.rb2d = GetComponent<Rigidbody2D>();
        if (_data.kkPos == null) _data.kkPos = FindFirstObjectByType<Kumkum>().transform;
    }

    public CientistStates GetNextState()
    {
        _stateListCounter++;
        return _stateList[_stateListCounter];
    }

    public CientistStates GetCurrentState()
    {
        return _stateList[_stateListCounter];
    }

    private void Update()
    {
        _fsm.Update();
    }

    private void FixedUpdate()
    {
        _fsm.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _fsm.OnTriggerEnter2D(collision);
    }

    public GameObject Create(GameObject obj)
    {
        return Instantiate(obj);
    }

    public bool CheckCurrentPosition()
    {
        float xPos = transform.position.x;

        switch (_data.targetPosition)
        {
            case -1:

                if (xPos < _data.leftMost.position.x)
                    return true;

                break;
            case 0:

                if (Mathf.Abs(transform.position.x - _data.middle.position.x) < _data.targetTolerance)
                    return true;

                break;
            case 1:

                if (xPos > _data.rightMost.position.x)
                    return true;

                break;
        }

        return false;
    }

    public void SelectRandomPosition()
    {
        var rand = Random.Range(0, 2);
        if (rand == 0) _data.targetPosition = -1;
        else _data.targetPosition = 1;
    }
}

public enum CientistStates
{
    LaserGunShoot,
    RayGunShoot,
    SpawnEnemy,
    Death,
    Vulnerable,
    Run
}