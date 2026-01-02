using System;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    State _currentState = null;

    Dictionary<Enum, State> _allStates = new Dictionary<Enum, State>();
    public void Update()
    {
        _currentState?.OnUpdate();
    }
    public void FixedUpdate()
    {
        _currentState?.OnFixedUpdate();
    }

    public void DrawGizmos()
    {
        _currentState.OnDrawGizmos();
    }

    public void AddState(Enum name, State state)
    {
        if (!_allStates.ContainsKey(name))
            _allStates.Add(name, state);
        else
            _allStates[name] = state;

        state.fsm = this;
    }

    public void ChangeState(Enum state)
    {
        _currentState?.OnExit();
        if (_allStates.ContainsKey(state))
            _currentState = _allStates[state];
        _currentState.OnEnter();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        _currentState.OnTriggerEnter(collision);
    }
}
