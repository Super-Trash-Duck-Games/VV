using UnityEngine;
using System.Collections;

public class AIGarEnemyView : AIEnemyView
{
    private AIGargEnemy _aiGar;
    public AIGarEnemyView(Animator anim, AIGargEnemy aigar) : base(anim)
    {
        _aiGar = aigar;
    }

    public void Attack(Vector2 dir)
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
        _aiGar.StartCoroutine(ResetPunchDir());
    }

    private IEnumerator ResetPunchDir()
    {
        yield return new WaitForSeconds(.5f);
        _anim.SetInteger("Punch", 0);
    }
}

