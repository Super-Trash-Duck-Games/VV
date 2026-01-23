using System.Collections;
using UnityEngine;

public class WaypointSaw : Saw
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _tolerance;
    [SerializeField] private float _speed;

    protected override void Start()
    {
        TurnOn();
        transform.position = _waypoints[0].position;
    }

    public void TurnOn()
    {
        _anim.SetTrigger("On");
        StartCoroutine(OnActivate());
    }

    public void TurnOff()
    {
        _anim.SetTrigger("Off");
        StopAllCoroutines();
    }

    private IEnumerator OnActivate()
    {
        int index = 0;
        while (true)
        {
            if (Vector2.Distance(transform.position, _waypoints[index].position) > _tolerance)
            {
                //transform.position = Vector2.Lerp(transform.position, _waypoints[index].position, _speed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, _waypoints[index].position, _speed * Time.deltaTime);
                yield return null;
            }
            else
            {
                if (index == 0) index = 1;
                else index = 0;
            }
            yield return null;
        }
    }
}
