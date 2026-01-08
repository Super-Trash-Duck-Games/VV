using System.Collections;
using UnityEngine;

public class AIZapEnemy : AIEnemy
{
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private GameObject _zone;
    [SerializeField] private float _electrifiedSpeed;

    private void Awake()
    {
        _cooldown += _ps.main.duration;
    }

    public override void Attack()
    {
        ZapZone();
    }

    private void ZapZone()
    {
        StartCoroutine(ZapZoneAttack());
    }

    private IEnumerator ZapZoneAttack()
    {
        var kk = FindFirstObjectByType<Kumkum>();
        _zone.SetActive(true);
        _ps.Play();
        view.SetBool("Electrified", true);

        float timer = 0;

        while (timer < _cooldown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(kk.transform.position.x, transform.position.y), _electrifiedSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        view.SetBool("Electrified", false);

        _zone.SetActive(false);
    }
}
