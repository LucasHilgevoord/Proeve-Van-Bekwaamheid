using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoCounter : State
{
    protected override void Start()
    {
        base.Start();
        WalkToCounter();
    }

    /// <summary>
    /// Move the npc to a preset position of the counter.
    /// </summary>
    private void WalkToCounter()
    {
        c.currentState = NpcStates.COUNTER;
        c.destination = PickCounter();
        c.agent.destination = c.destination.position;
    }

    private Transform PickCounter()
    {
        // Check if the standpoint is not occupied;
        int pickedPoint = -1;
        for (int i = 0; i < c.manager.counters.standingPoints.Length; i++)
        {
            if (!c.manager.counters.standingPoints[i].isOccupied)
            {
                pickedPoint = i;
                c.manager.counters.standingPoints[i].isOccupied = true;
                break;
            }
        }
        
        if (pickedPoint < 0)
        {
            // No counter spots availible
            c.ChangeState(NpcStates.LEAVE);
        }
        else
        {
            // Found availible counter spot
            c.destination = c.manager.counters.standingPoints[pickedPoint].standingPoint;
            c.destinationPoint = pickedPoint;
        }
        return c.destination;
    }
}
