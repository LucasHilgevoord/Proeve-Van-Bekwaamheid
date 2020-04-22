using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateID
{
    public NpcStates stateName;
    public Type stateScript;
    public StateID(NpcStates state, Type script)
    {
        stateName = state;
        stateScript = script;
    }
}

public class StateMachine : MonoBehaviour
{
    private Dictionary<NpcStates, Type> states = new Dictionary<NpcStates, Type>();
    private State _currentState = null;

    /// <summary>
    /// Add a state to the StateMachine.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    protected void AddState(NpcStates id, Type state)
    {
        states.Add(id, state);
    }

    /// <summary>
    /// Change the current state to the assigned new state.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="args"></param>
    public void ChangeState(NpcStates id)
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
