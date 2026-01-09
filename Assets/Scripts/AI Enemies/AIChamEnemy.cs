using System.Collections;
using UnityEngine;

public class AIChamEnemy : AIEnemy
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform _tonguePos;
    [SerializeField] private float _tongueSpeed;
    [SerializeField] private float _tongueTime;
    [SerializeField] private float _tongueDelay;


    protected override void OnStart()
    {
        base.OnStart();
        _lr.SetPosition(0, Vector2.zero);
        _lr.SetPosition(1, Vector2.zero);
        _cooldown += _tongueTime;
    }

    public override void Attack()
    {
        TonguePunch();
    }

    public void TonguePunch()
    {
        view.Attack();
        _lr.SetPosition(0, _tonguePos.position);
        _lr.SetPosition(1, _tonguePos.position);
        StartCoroutine(TongueAttack());
    }

    private IEnumerator TongueAttack()
    {
        var kum = FindFirstObjectByType<Kumkum>().transform.position;
        _lr.SetPosition(1, _tonguePos.position);
        float timer = 0;
        Vector2 tongue = _tonguePos.position;


        yield return new WaitForSeconds(_tongueDelay);
        while (timer < _tongueTime)
        {
            timer += Time.deltaTime * _tongueSpeed;
            _lr.SetPosition(1, tongue);
            if (timer < _tongueTime / 2)
            {
                tongue = Vector2.Lerp(_tonguePos.position, kum, timer);
            }
            else if (timer > _tongueTime / 2)
                tongue = Vector2.Lerp(kum, _tonguePos.position, timer);

            if (Physics2D.Linecast(_tonguePos.position, tongue, _kkLM))
                DeathTongue(tongue);

            yield return null;
        }

        _lr.SetPosition(0, Vector2.zero);
        _lr.SetPosition(1, Vector2.zero);

    }

    private void DeathTongue(Vector2 spawnPos)
    {
        var deathRay = Instantiate(new GameObject());
        deathRay.transform.position = spawnPos;
        var col = deathRay.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.gameObject.layer = 7;
        Destroy(deathRay, _cooldown);
    }
}
