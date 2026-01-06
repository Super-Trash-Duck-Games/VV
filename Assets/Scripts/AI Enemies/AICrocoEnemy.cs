using Unity.Mathematics;
using UnityEngine;

public class AICrocoEnemy : AIEnemy
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private CrocoBullet _bulletPrefab;
    [SerializeField] private float _shootCooldown;
    protected override void SetupFSM()
    {
        _fsm = new FiniteStateMachine();

        _fsm.AddState(AIEnemiesStates.Patrol, new PatrolState(this, _waypoints, _rb2d, _movementPackage.GetComponent<EntityPackage>()));
        _fsm.AddState(AIEnemiesStates.PatrolPoint, new PatrolPointState(this, _waitTime));
        _fsm.AddState(AIEnemiesStates.Dizzy, new DizzyState(this, _dizzyTime, view));
        _fsm.AddState(AIEnemiesStates.Attack, new CRAttackState(this, _shootCooldown));

        _fsm.ChangeState(AIEnemiesStates.Patrol);
    }

    public CrocoBullet Shoot()
    {
        var crocobullet = Instantiate(_bulletPrefab, _shootPoint.position, quaternion.identity);
        if (transform.localScale.x < 0) crocobullet.transform.localScale = new Vector2(-1, 1);

        return crocobullet;
    }
}
