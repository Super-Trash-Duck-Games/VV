using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _buildUp;
    [Range(0f, 1f)]
    [SerializeField] private float _damage;
    [SerializeField] private Color _initialColor, _damagedColor;
    [SerializeField] private SpriteRenderer _gemSR;
    [SerializeField] private float _damagedEmmisionAmount;
    private ParticleSystem.EmissionModule emmision;
    private Coroutine _updatDamageDisplay;
    void Start()
    {
        emmision = _buildUp.emission;
    }

    //private void Update()
    //{
    //    emmision.rateOverTime = Mathf.Lerp(0, _damagedEmmisionAmount, _damage);
    //    _gemSR.color = Color.Lerp(_initialColor, _damagedColor, _damage);
    //}

    public void UpdateDamageAmount(float damage)
    {
        _damage = damage;
        if (_updatDamageDisplay == null) _updatDamageDisplay = StartCoroutine(UpdateDamageDisplay());
    }

    private IEnumerator UpdateDamageDisplay()
    {
        while (true)
        {
            yield return null;
            emmision.rateOverTime = Mathf.Lerp(0, _damagedEmmisionAmount, _damage);
            _gemSR.color = Color.Lerp(_initialColor, _damagedColor, _damage);
        }
    }
}
