using System.Collections;
using UnityEngine;

public class AcidTank : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _acidContent;
    private Vector2 _originalSize;
    [SerializeField] private Watcher[] _showerWatchers;

    [SerializeField] private float _depletionSpeed;
    [SerializeField] private bool _depleting;
    public bool _depleted;
    [SerializeField] private int _depletingMultiplier;
    private Coroutine _depletingCr;

    [SerializeField] private float _refillSpeed;
    [SerializeField] private bool _refilling;
    void Start()
    {
        _originalSize = _acidContent.size;

        foreach (var watcher in _showerWatchers)
        {
            watcher.OnActivation += OnShowerActivation;
        }
    }

    private IEnumerator Depleting()
    {
        while (_depleting)
        {
            _acidContent.size -= Vector2.up * Time.deltaTime * _depletionSpeed * _depletingMultiplier;
            if (_acidContent.size.y <= 0)
            {
                _depleted = true;
                StartCoroutine(Replete());
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator Replete()
    {
        _refilling = true;
        while (_acidContent.size.y < _originalSize.y)
        {
            _acidContent.size += Vector2.up * Time.deltaTime * _refillSpeed ;

            yield return null;
        }
        _depleted = false;
        _refilling = false;
    }


    private void OnShowerActivation(bool showerActivation)
    {
        if (showerActivation)
        {
            _depletingMultiplier++;
            _depleting = showerActivation;
        }
        else
        {
            _depletingMultiplier--;
            if (_depletingMultiplier == 0)
            {
                StopCoroutine(_depletingCr);
                _depletingCr = null;
                return;
            }
        }

        if (!_refilling && _depletingMultiplier > 0 && _depletingCr == null) _depletingCr = StartCoroutine(Depleting());
    }


}
