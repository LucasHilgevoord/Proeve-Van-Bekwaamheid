using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoStation : State
{

    protected override void Start()
    {
        base.Start();
        WalkToStation();
    }

    /// <summary>
    /// Move the npc to a preset position of a buy station.
    /// </summary>
    public void WalkToStation()
    {
        c.currentState = NpcStates.states.BUYSTATION;
        c.destination = PickStation();
        c.agent.destination = c.destination.position;
    }

    /// <summary>
    /// Pick a buy station where the npc wants to go.
    /// </summary>
    private Transform PickStation()
    {
        // Pick random station from list of availible stations
        int pickedStation = Random.Range(0, c.manager.buyStations.Count);

        // Check if the standpoint is not occupied;
        int pickedPoint = -1;
        for (int i = 0; i < c.manager.buyStations[pickedStation].standingPoints.Length; i++)
        {
            if (!c.manager.buyStations[pickedStation].standingPoints[i].isOccupied)
            {
                pickedPoint = i;
                c.manager.buyStations[pickedStation].standingPoints[i].isOccupied = true;
                break;
            }
        }

        // If no standpoint is availible
        if (pickedPoint < 0)
        {
            if (c.curPickCount < c.maxPickCount)
            {
                // Try finding another availible stand
                c.curPickCount++;
                c.destination = PickStation();
            }
            else
            {
                // No stands availible within max tries
                c.ChangeState(NpcStates.states.LEAVE);
            }
        }
        else
        {
            // Found an availible stand
            c.destination = c.manager.buyStations[pickedStation].standingPoints[pickedPoint].standingPoint;

            // Saving location
            c.destinationStation = pickedStation;
            c.destinationPoint = pickedPoint;
        }
        return c.destination;
    }


}
