using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;
    private WorldManager manager;

    [SerializeField]
    private NpcGoals.goals purpose; // Purpose of the npc that is entering the store.
    [SerializeField]
    private NpcStates.states currentState; // Current state the npc is in.

    private Transform destination;
    private int maxPickCount = 10; // Maximal number of checks the npc can do for an availible station.
    private int curPickCount = 0; // Current number of checks the npc did for an availible station.


    // Start is called before the first frame update
    void Start()
    {
        manager = WorldManager.SharedInstance;
        agent = GetComponent<NavMeshAgent>();

        if (purpose == NpcGoals.goals.LOOKAROUND || purpose == NpcGoals.goals.BUY)
        {
            WalkToStation();
        } else if (purpose == NpcGoals.goals.REPAIR || purpose == NpcGoals.goals.SELL)
        {
            WalkToCounter();
        }
    }

    #region BuyStation
    /// <summary>
    /// Move the npc to a preset position of a buy station.
    /// </summary>
    private void WalkToStation()
    {
        destination = PickStation();
        agent.destination = destination.position;
        currentState = NpcStates.states.BUYSTATION;
    }

    /// <summary>
    /// Pick a buy station where the npc wants to go.
    /// </summary>
    private Transform PickStation()
    {
        Transform destination = transform;

        // Pick random station from list
        int pickedStation = Random.Range(0, manager.buyStations.Count);

        // Check if the standpoint is not occupied;
        int pickedPoint = -1;
        for (int i = 0; i < manager.buyStations[pickedStation].standingPoints.Length; i++)
        {
            if (!manager.buyStations[pickedStation].standingPoints[i].isOccupied)
            {
                pickedPoint = i;
                manager.buyStations[pickedStation].standingPoints[i].isOccupied = true;
                break;
            }
        }

        if (pickedPoint < 0 && curPickCount < maxPickCount)
        {
            //Try finding another availible stand
            curPickCount++;
            PickStation();
        } else if (pickedPoint > 0)
        {
            //Found an availible stand
            destination = manager.buyStations[pickedStation].standingPoints[0].standingPoints;
        } else
        {
            //No stands availible within max tries
            Leave();
        }
        return destination;
    }
    #endregion

    /// <summary>
    /// Make the npc stand still and look at something.
    /// </summary>
    private void LookAtStuff()
    {
        currentState = NpcStates.states.CHECKING;
    }

    /// <summary>
    /// Move the npc to a preset position of the counter.
    /// </summary>
    private void WalkToCounter()
    {
        currentState = NpcStates.states.COUNTER;
    }

    /// <summary>
    /// Make the npc wait until the player clicked on him/her.
    /// After that open the window that belongs to it.
    /// </summary>
    private void Checkout()
    {
        currentState = NpcStates.states.CHECKOUT;
    }

    /// <summary>
    /// Move the npc to the exit and fade him/her out once reached.
    /// </summary>
    private void Leave()
    {
        currentState = NpcStates.states.LEAVE;
    }

    /// <summary>
    /// Open a dialogue window when clicked on the npc.
    /// </summary>
    private void Talk()
    {
        currentState = NpcStates.states.TALK;
    }
}
