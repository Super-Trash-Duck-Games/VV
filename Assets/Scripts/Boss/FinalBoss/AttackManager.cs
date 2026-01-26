using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    /*[SerializeField] private GameObject _slamHandPrefab;

    private GameObject _slam1, _slam2;

    [SerializeField] private Transform[] _slamSpots;
    [SerializeField] private float _groundLevel;
    [SerializeField] private float _slamSpeed;
    [SerializeField] private float _delayBetweenSlams;

    void Start()
    {
        _slam1 = Instantiate(_slamHandPrefab);
        _slam1.transform.position = new Vector2(0, -10);
        _slam2 = Instantiate(_slamHandPrefab);
        _slam2.transform.position = new Vector2(0, -10);
        _slam2.transform.localScale = new Vector2(-1, 1);

        StartCoroutine(SlamSequence());
    }

    private IEnumerator SlamSequence()
    {
        int counter = 0;

        while (counter < _slamSpots.Length)
        {
            _slam1.transform.position = _slamSpots[counter].position;
            yield return new WaitForSeconds(_delayBetweenSlams);

            while (_slam1.transform.position.y > _groundLevel)
            {
                _slam1.transform.position -= Vector3.up * _slamSpeed * Time.deltaTime;
                yield return null;
            }
            counter++;

            _slam2.transform.position = _slamSpots[counter].position;
            yield return new WaitForSeconds(_delayBetweenSlams);

            while (_slam2.transform.position.y > _groundLevel)
            {
                _slam2.transform.position -= Vector3.up * _slamSpeed * Time.deltaTime;
                yield return null;
            }
            counter++;

        }
        _slam1.transform.position = new Vector2(0, -10);
        _slam2.transform.position = new Vector2(0, -10);

    }*/

    [SerializeField] private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _anim.SetTrigger("Slam");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _anim.SetTrigger("FistCrush");
    }
#endif

}
