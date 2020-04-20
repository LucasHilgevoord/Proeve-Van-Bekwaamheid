using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoLeave : State
{
    protected override void Start()
    {
        base.Start();
        Leave();
    }

    /// <summary>
    /// Move the npc to the exit and fade him/her out once reached.
    /// </summary>
    private void Leave()
    {
        c.currentState = NpcStates.states.LEAVE;
    }
}
