using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoCheck : State
{
    protected override void Start()
    {
        base.Start();
        LookAtStuff();
    }

    /// <summary>
    /// Make the npc stand still and look at something.
    /// </summary>
    private void LookAtStuff()
    {
        c.currentState = NpcStates.CHECKING;
    }
}
