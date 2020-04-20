using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateID
{
    public NpcStates.states stateName;
    public Type stateScript;
    public StateID(NpcStates.states state, Type script)
    {
        stateName = state;
        stateScript = script;
    }
}

public class StateMachine : MonoBehaviour
{
    private Dictionary<NpcStates.states, Type> states = new Dictionary<NpcStates.states, Type>();
    private State _currentState = null;

    /// <summary>
    /// Add a state to the StateMachine.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    protected void AddState(NpcStates.states id, Type state)
    {
        states.Add(id, state);
    }

    /// <summary>
    /// Change the current state to the assigned new state.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="args"></param>
    public void ChangeState(NpcStates.states id)
    {
        Destroy(_currentState);
        if (!states.ContainsKey(id))
        {
            _currentState = null;
            return;
        }
        _currentState = this.gameObject.AddComponent(states[id]) as State;
    }
}
