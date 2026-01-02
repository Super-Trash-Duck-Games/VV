using UnityEngine;

public class AIEnemyView 
{
    private Animator _anim;
    public AIEnemyView(Animator anim)
    {
        _anim = anim;
    }

    public void Move(bool moving)
    {
        _anim.SetBool("Moving", moving);
    }

    public void Dizzy(bool dizzy)
    {
        _anim.SetBool("Dizzy", dizzy);
    }
}
