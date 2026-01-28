using System.Collections;
using UnityEngine;

public class CientificForceField : MonoBehaviour
{
    [SerializeField] private ParticleSystem _forceFieldPS;
    [SerializeField] private AreaEffector2D _forceArea;
    [SerializeField] private Collider2D _forceFieldCollider;

    [SerializeField] private Kumkum _kk;
    [SerializeField] private bool _ffActive;

    private void Awake()
    {
        if(_forceFieldPS == null) _forceFieldPS = GetComponent<ParticleSystem>();
        if(_forceArea == null ) _forceArea = GetComponent<AreaEffector2D>();
        if(_forceFieldCollider == null) _forceFieldCollider = GetComponent<Collider2D>();

        _kk = FindFirstObjectByType<Kumkum>();
    }

    void Start()
    {
        _forceFieldCollider.enabled = false;
    }

    public void ActivateForceField()
    {
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        _ffActive = true;

        _forceFieldCollider.enabled = true;
        _forceFieldPS.Play();
        while (_ffActive)
        {
            Vector2 forceFieldDirection = _kk.transform.position - transform.position;
            _forceArea.forceAngle = Mathf.Atan2(forceFieldDirection.y, forceFieldDirection.x) * Mathf.Rad2Deg;
            yield return null;
        }

        _forceFieldCollider.enabled = false;
        _forceFieldPS.Stop();
    }
}
