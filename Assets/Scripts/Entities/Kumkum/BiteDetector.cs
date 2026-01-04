using System;
using UnityEngine;

public class BiteDetector : MonoBehaviour
{
    private Collider2D _collider;
    public Action<AIEnemy> OnDetectEnemy;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        TurnOff();
    }

    public void TurnOn()
    {
        _collider.enabled = true;
    }

    public void TurnOff()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            AIEnemy enemy = collision.gameObject.GetComponent<AIEnemy>();
            if (enemy.isDizzy)
                OnDetectEnemy?.Invoke(enemy);
        }
    }
}
