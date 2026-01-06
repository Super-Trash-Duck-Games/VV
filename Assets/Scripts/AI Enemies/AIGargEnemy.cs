using UnityEngine;
using System.Collections;

public class AIGargEnemy : AIEnemy
{
    [SerializeField] private float _cooldown;
    [SerializeField] private GPackage _gp;
    private AIGarEnemyView _garView;
    [SerializeField] private Collider2D[] _hitColliders;
    protected override void SetupFSM()
    {
        _fsm = new FiniteStateMachine();

        _fsm.AddState(AIEnemiesStates.Patrol, new PatrolState(this, _waypoints, _rb2d, _movementPackage.GetComponent<EntityPackage>()));
        _fsm.AddState(AIEnemiesStates.PatrolPoint, new PatrolPointState(this, _waitTime));
        _fsm.AddState(AIEnemiesStates.Dizzy, new DizzyState(this, _dizzyTime, view));
        _fsm.AddState(AIEnemiesStates.Attack, new GarAttackState(this, _cooldown));

        _fsm.ChangeState(AIEnemiesStates.Patrol);
    }

    protected override void SetupComponents()
    {
        base.SetupComponents();
        _garView = new AIGarEnemyView(_anim, this);
    }

    public void Punch()
    {
        Transform kkpos = FindFirstObjectByType<Kumkum>().transform;

        if (kkpos.position.y > transform.position.y)
        {
            StartCoroutine(Dash(new Vector2(0, 1), _hitColliders[1]));
        }
        else if (kkpos.position.x < transform.position.x)
        {
            StartCoroutine(Dash(new Vector2(-1, 0),  _hitColliders[0]));

        }
        else if (kkpos.position.x > transform.position.x)
        {
            StartCoroutine(Dash(new Vector2(1, 0), _hitColliders[0]));
        }
    }

    private IEnumerator Dash(Vector2 direction, Collider2D hitbox)
    {
        hitbox.enabled = true;
        float counter = 0;
        _garView.Attack(direction);

        yield return new WaitForSeconds(_gp.dashDelay);
        _rb2d.AddForce(direction * _gp.punchDash, ForceMode2D.Impulse);

        while (counter < _gp.hitDuration)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        hitbox.enabled = false;

    }

}
