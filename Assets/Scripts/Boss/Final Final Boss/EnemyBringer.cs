using System.Collections;
using UnityEngine;

public class EnemyBringer : MonoBehaviour
{
    [SerializeField] private AIEnemy[] _aIEnemies;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _placer;
    [SerializeField] private float _speed;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _pause;
    void Start()
    {
        foreach (var point in _waypoints) point.transform.parent = null;
    }

    public void Bring(bool right)
    {
        StartCoroutine(BringEnemy(right));
    }


    private IEnumerator BringEnemy(bool right)
    {
        transform.position = new Vector3(_placer.position.x, 12);

        int rand = Random.Range(0, _aIEnemies.Length - 1);
        AIEnemy enemy = Instantiate(_aIEnemies[rand]);
        enemy.transform.position = transform.position + transform.up * 2;
        enemy.enabled = false;
        enemy.transform.parent = transform;

        enemy.waypoints.Clear();
        enemy.SetWaypoints(_waypoints);

        while (transform.position.y > _waypoints[0].position.y)
        {
            transform.position -= transform.up * _speed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_pause);
        _collider.enabled = false;
        enemy.enabled = true;
        enemy.transform.parent = null;

        while (transform.position.y > -5)
        {
            transform.position -= transform.up * _speed * 2 * Time.deltaTime;
            yield return null;
        }
    }
}
