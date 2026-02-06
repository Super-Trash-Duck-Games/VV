using System.Collections;
using UnityEngine;

public class CDeathState : State
{
    private Cientist _cientist;
    private CData _data;

    public CDeathState(Cientist cientist, CData data)
    {
        _cientist = cientist;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientist._currentState = CientistStates.Death;

        _cientist.anim.SetTrigger("Death");
        _data.ff.DeactivateForceField();

        _cientist.StartCoroutine(EndDoorDelay());
    }

    private IEnumerator EndDoorDelay()
    {
        yield return new WaitForSeconds(_data.endDoorDelay);

        _data.endDoor.Activate();
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
    }
}
