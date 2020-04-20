using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent agent;
    private WorldManager manager;

    public NpcGoals.goals purpose; // Purpose of the npc that is entering the store.
    public NpcStates.states currentState; // Current state the npc is in.
    private NpcStates.states prevState;

    [SerializeField]
    private GameObject conversationObj; // Link to object which shows the conversation
    [SerializeField]
    private GameObject checkoutObj; // Link to object which shows the menu when you checkout
    [SerializeField]
    private SpriteRenderer checkoutIconObj; // Link to icon object shown when NPC reaches the counter
    [SerializeField]
    private Sprite[] checkoutIcon; // Icons which gets shown when NPC is at the counter
    private bool hasInteracted;

    private Vector3 myPos;
    private Vector3 destPos;

    private Transform destination; // destination that has been chosen to go to.
    private int destinationStation; // Station that hase been chosen to go to.
    private int destinationPoint; // Standing point that has been chosen to go to.
    private float destinationRange = 1f; // Max distance between npc and distance to have reached it. 

    private int maxPickCount = 20; // Maximal number of checks the npc can do for an availible station.
    private int curPickCount = 0; // Current number of checks the npc did for an availible station.

    private float maxLookTime = 10; // Maximal time the npc can look at items at a buying station.
    private float minLookTime = 5; // Minimal time the npc should look at items at a buying station.
    private float curLookTime; // Current time the npc has been looking at the items.

    private int maxStationChange = 0; // Amount of times the npc can look at another station after the previous one.
    private int curStationChange = 0; // Current amount of times the npc changed station.

    #region Main
    // Start is called before the first frame update
    void Start()
    {
        manager = WorldManager.SharedInstance;
        agent = GetComponent<NavMeshAgent>();
        curLookTime = Random.Range(minLookTime, maxLookTime);
        destination = transform;

        purpose = NpcGoals.goals.BUY;

        //Defining start states.
        if (purpose == NpcGoals.goals.LOOKAROUND || purpose == NpcGoals.goals.BUY)
        {
            WalkToStation();
        } else if (purpose == NpcGoals.goals.REPAIR || purpose == NpcGoals.goals.SELL)
        {
            WalkToCounter();
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case NpcStates.states.BUYSTATION:
                myPos = new Vector3(transform.position.x, 0, transform.position.z);
                destPos = new Vector3(destination.position.x, 0, destination.position.z);
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    //When npc is close enough to the station point, make it look at the items.
                    LookAtStuff();
                }
                break;
            case NpcStates.states.CHECKING:
                //Countdown for when npc the stops looking at items.
                curLookTime -= Time.deltaTime;
                if (curLookTime < 0)
                {
                    int choice = Random.Range(0, 2); 
                    manager.buyStations[destinationStation].standingPoints[destinationPoint].isOccupied = false;
                    if (choice == 0 && curStationChange < maxStationChange)
                    {
                        //Pick another station to look at.
                        WalkToStation();
                        curStationChange++;
                    } else
                    {
                        //Go to the next state if is done looking around
                        if (purpose == NpcGoals.goals.BUY)
                        {
                            WalkToCounter();
                        } else
                        {
                            Leave();
                        }
                    }
                    curLookTime = Random.Range(minLookTime, maxLookTime);
                }
                break;
            case NpcStates.states.COUNTER:
                myPos = new Vector3(transform.position.x, 0, transform.position.z);
                destPos = new Vector3(destination.position.x, 0, destination.position.z);
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    Checkout();
                }
                break;
            case NpcStates.states.CHECKOUT:
                break;
            case NpcStates.states.LEAVE:
                break;
            case NpcStates.states.TALK:
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (hasInteracted) return;

        if (currentState == NpcStates.states.CHECKOUT)
        {
            checkoutObj.SetActive(true);
        }
        else
        {
            Talk();
        }
    }
    #endregion

    #region BuyStation
    /// <summary>
    /// Move the npc to a preset position of a buy station.
    /// </summary>
    private void WalkToStation()
    {
        currentState = NpcStates.states.BUYSTATION;
        destination = PickStation();
        agent.destination = destination.position;
    }

    /// <summary>
    /// Pick a buy station where the npc wants to go.
    /// </summary>
    private Transform PickStation()
    {
        // Pick random station from list of availible stations
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

        // If no standpoint is availible
        if (pickedPoint < 0)
        {
            if (curPickCount < maxPickCount)
            {
                // Try finding another availible stand
                curPickCount++;
                destination = PickStation();
            }
            else
            {
                // No stands availible within max tries
                destination = transform;
                Leave();
            }
        }
        else
        {
            // Found an availible stand
            destination = manager.buyStations[pickedStation].standingPoints[pickedPoint].standingPoint;
            destinationStation = pickedStation;
            destinationPoint = pickedPoint;
        }
        return destination;
    }
    #endregion

    #region Checking
    /// <summary>
    /// Make the npc stand still and look at something.
    /// </summary>
    private void LookAtStuff()
    {
        currentState = NpcStates.states.CHECKING;
    }
    #endregion

    #region Counter
    /// <summary>
    /// Move the npc to a preset position of the counter.
    /// </summary>
    private void WalkToCounter()
    {
        currentState = NpcStates.states.COUNTER;
        destination = PickCounter();
        agent.destination = destination.position;
        Debug.Log("Set counter destination");
    }

    private Transform PickCounter()
    {
        // Check if the standpoint is not occupied;
        int pickedPoint = -1;
        for (int i = 0; i < manager.counters.standingPoints.Length; i++)
        {
            if (!manager.counters.standingPoints[i].isOccupied)
            {
                pickedPoint = i;
                manager.counters.standingPoints[i].isOccupied = true;
                break;
            }
        }

        //If counter places are full
        if (pickedPoint < 0)
        {
            //destination = PickCounter();
        } else
        {
            destination = manager.counters.standingPoints[pickedPoint].standingPoint;
        }
        return destination;
    }
    #endregion

    #region Checkout
    /// <summary>
    /// Make the npc wait until the player clicked on him/her.
    /// Opens the checkout window after that.
    /// </summary>
    private void Checkout()
    {
        currentState = NpcStates.states.CHECKOUT;
        checkoutIconObj.gameObject.SetActive(true);
        checkoutIconObj.sprite = checkoutIcon[(int)purpose];
    }
    #endregion

    #region Leave
    /// <summary>
    /// Move the npc to the exit and fade him/her out once reached.
    /// </summary>
    private void Leave()
    {
        currentState = NpcStates.states.LEAVE;
    }
    #endregion

    #region Talk
    /// <summary>
    /// Open a dialogue window when clicked on the npc.
    /// </summary>
    private void Talk()
    {
        prevState = currentState;
        agent.destination = transform.position;
        currentState = NpcStates.states.TALK;
        conversationObj.SetActive(true);
    }
    #endregion
}
