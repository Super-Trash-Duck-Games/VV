using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public EntityTypes type;
    [Header("MVC")]
    protected Model _model;
    protected View _view;
    protected Controller _controller;

    [Header("Components")]
    protected Rigidbody2D _rb2d;
    protected Animator _anim;

    [Header("Parameters")]
    [SerializeField] protected GameObject _mpGO;
    protected MovementPackage _mp;

    [Header("Ground Detection")]
    [field: SerializeField] public bool Grounded { get; protected set; }
    public Action<bool> OnGrounded;

    protected virtual void Start()
    {
        _mp = _mpGO.GetComponent<MovementPackage>();
        GetComponents();
        MVC();
    }

    protected virtual void MVC()
    {
        _model = new Model(this, _rb2d, _mp);
        _view = new View(_anim, this, _model);
        _controller = new Controller(_model);
    }

    protected virtual void GetComponents()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            if (rb2d != null)
                _rb2d = rb2d;
            else
                _rb2d = gameObject.AddComponent<Rigidbody2D>();

            _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        _anim = GetComponentInChildren<Animator>();

    }

    protected virtual void Update()
    {
        _controller.FauxUpdate();

    }

    protected virtual void LateUpdate()
    {
        _controller.FauxLateUpdate();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Grounded = true;
            OnGrounded?.Invoke(true);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            OnGrounded?.Invoke(false);
            Grounded = false;
        }
    }

    public virtual void MorphBack(Entity kumkum)
    {
        StartCoroutine(ActivateKumkum(_view.MorphBack(), kumkum));
    }

    private IEnumerator ActivateKumkum(float morphTime, Entity kumkum)
    {
        yield return new WaitForSeconds(morphTime);
        gameObject.SetActive(false);
        kumkum.gameObject.SetActive(true);
    }
}
