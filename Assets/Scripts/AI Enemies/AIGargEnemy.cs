using UnityEngine;
using System.Collections;

public class AIGargEnemy : AIEnemy
{
    [SerializeField] private GPackage _gp;
    private AIGarEnemyView _garView;
    [SerializeField] private Collider2D[] _hitColliders;

    protected override void SetupComponents()
    {
        base.SetupComponents();
        _garView = new AIGarEnemyView(_anim, this);
        _rb2d.mass = .5f;
        _rb2d.linearDamping = .5f;
        _rb2d.angularDamping = .5f;
    }

    public override void Attack()
    {
        Punch();
    }

    public void Punch()
    {
        Transform kkpos = FindFirstObjectByType<Kumkum>().transform;

        if (kkpos.position.x < transform.position.x)
        {
            StartCoroutine(Dash(new Vector2(-1, 0), _hitColliders[0]));

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
