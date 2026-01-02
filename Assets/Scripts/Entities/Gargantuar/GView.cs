using System.Collections;
using UnityEngine;

public class GView : View
{
    private GModel _gModel;
    private Gargantuar _gargantuar;
    public GView(Animator anim, Gargantuar entity, GModel model) : base(anim, entity, model)
    {
        _anim = anim;
        _gargantuar = entity;
        _gModel = model;

        _gModel.OnMove += Move;
        _gModel.OnJump += Jump;
        _gModel.OnFall += Fall;
        _gargantuar.OnGrounded += Grounded;
        _gModel.SpecialDir += Special;
    }

    private void Special(Vector2 dir)
    {
        if (dir.x > 0)
        {
            _anim.SetInteger("Punch", 2);
            _anim.SetTrigger("Special");
        }
        else if (dir.x < 0)
        {
            _anim.SetInteger("Punch", 2);
            _anim.SetTrigger("Special");
        }
        else if (dir.y > 0)
        {
            _anim.SetInteger("Punch", 1);
            _anim.SetTrigger("Special");
        }
        else if (dir.y < 0)
        {
            _anim.SetInteger("Punch", 3);
            _anim.SetTrigger("Special");
        }
        _gargantuar.StartCoroutine(ResetPunchDir());
    }

    private IEnumerator ResetPunchDir()
    {
        yield return new WaitForSeconds(.5f);
        _anim.SetInteger("Punch", 0);
    }
}
