using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoLeave : State
{
    public delegate void Leaving(NpcController npc);
    public static event Leaving OnNpcLeave;

    private bool hasLeft;

    protected override void Start()
    {
        base.Start();
        Leave();
    }

    private void Update()
    {
        if (!hasLeft && Vector2.Distance(c.myPos, c.destPos) < c.destinationRange)
        {
            OnNpcLeave(c);
            hasLeft = true;
        }
    }

    /// <summary>
    /// Move the npc to the exit.
    /// </summary>
    private void Leave()
    {
        // If the npc was standing at the counter
        if (c.currentState == NpcStates.states.CHECKOUT)
        {
            c.manager.counters.standingPoints[c.destinationPoint].isOccupied = false;
            c.checkoutIconObj.gameObject.SetActive(false);
        }

        // Move NPC
        c.currentState = NpcStates.states.LEAVE;
        c.destination = c.manager.door;
        c.agent.destination = c.destination.position;
    }
}
