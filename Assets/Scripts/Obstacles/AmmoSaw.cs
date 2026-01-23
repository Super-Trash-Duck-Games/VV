using System.Collections;
using UnityEngine;

public class AmmoSaw : Saw
{
    [SerializeField] private float _speed;


    public void Shoot(Vector2 up)
    {
        StartCoroutine(OnShoot());
        transform.up = up;
    }

    private IEnumerator OnShoot()
    {
        while (true)
        {
            transform.position += transform.up * _speed * Time.deltaTime;
            yield return null;
        }
    }
}
