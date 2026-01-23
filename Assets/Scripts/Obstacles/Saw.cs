using System.Collections;
using UnityEngine;

public class Saw : MonoBehaviour, IHazard
{
    [SerializeField] protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("On");
    }

    public virtual void Activate()
    {
        
    }

    public virtual void DeActivate()
    {
    }
}
