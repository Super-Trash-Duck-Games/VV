using UnityEngine;

public abstract class State
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();

    public abstract void OnDrawGizmos();
    public abstract void OnTriggerEnter(Collider2D collision);

    public FiniteStateMachine fsm;
}
