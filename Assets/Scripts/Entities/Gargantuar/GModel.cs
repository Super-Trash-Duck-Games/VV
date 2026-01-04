using System;
using System.Collections;
using UnityEngine;

public class GModel : Model
{
    private Gargantuar _gargantuar;
    public Action<Vector2> SpecialDir;
    private GameObject[] _hitboxes;
    private Coroutine _hit;
    private GPackage _gp;

    public GModel(Gargantuar entity, Rigidbody2D rb2d, GPackage gp, GameObject[] hitboxes) : base(entity, rb2d, gp)
    {
        _gargantuar = entity;
        _rb2d = rb2d;
        _gp = gp;

        _gargantuar.OnGrounded += OnGrounded;
        _hitboxes = hitboxes;
    }

    public override void JumpRelease()
    {
        if (_hit != null) return;

        base.JumpRelease();
    }

    public void Special(Vector2 direction)
    {
        if (_hit != null) return;
        GameObject hitbox = _hitboxes[0];
        if (direction.x != 0)
        {
           hitbox = _hitboxes[0];
        }
        else if (direction.y > 0)
        {
            hitbox = _hitboxes[1];
        }
        else if (direction.y < 0)
        {
            hitbox = _hitboxes[2];
        }
        _hit = _gargantuar.StartCoroutine(Dash(direction, hitbox));
    }

    private IEnumerator Dash(Vector2 direction, GameObject hitbox)
    {
        hitbox.SetActive(true);
        float counter = 0;
        SpecialDir?.Invoke(direction);

        yield return new WaitForSeconds(_gp.dashDelay);
        _rb2d.AddForce(direction * _gp.punchDash, ForceMode2D.Impulse);

        while (counter < _gp.hitDuration)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        hitbox.SetActive(false);
        _hit = null;
    }
}