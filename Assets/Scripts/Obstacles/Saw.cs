using System.Collections;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _tolerance;
    [SerializeField] private float _speed;

    void Start()
    {
        _anim = GetComponent<Animator>();
        TurnOn();
        transform.position = _waypoints[0].position;
    }

    private IEnumerator Activate()
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

    public void TurnOn()
    {
        _anim.SetTrigger("On");
        StartCoroutine(Activate());
    }

    public void TurnOff()
    {
        _anim.SetTrigger("Off");
        StopAllCoroutines();
    }
}
