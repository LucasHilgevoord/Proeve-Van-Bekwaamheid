using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : StateMachine
{
    [Header("Managers")]
    public NavMeshAgent agent;
    public WorldManager manager;

    [Header("States")]
    public NpcGoals purpose; // Purpose of the npc that is entering the store.
    public NpcStates currentState; // Current state the npc is in.
    public NpcStates prevState;
    private StateID[] states = {
        new StateID(NpcStates.BUYSTATION, typeof(NpcGoStation)),
        new StateID(NpcStates.CHECKING, typeof(NpcGoCheck)),
        new StateID(NpcStates.COUNTER, typeof(NpcGoCounter)),
        new StateID(NpcStates.CHECKOUT, typeof(NpcGoCheckout)),
        new StateID(NpcStates.LEAVE, typeof(NpcGoLeave)),
        new StateID(NpcStates.TALK, typeof(NpcGoTalk)),
    };

    [Header("Interaction")]
    public GameObject conversationObj; // Link to object which shows the conversation
    public GameObject checkoutObj; // Link to object which shows the menu when you checkout
    public SpriteRenderer checkoutIconObj; // Link to icon object shown when NPC reaches the counter
    public Sprite[] checkoutIcon; // Icons which gets shown when NPC is at the counter
    public bool hasInteracted;

    [Header("Destination")]
    public Vector3 myPos;
    public Vector3 destPos;
    public Transform destination; // destination that has been chosen to go to.
    public float destinationRange = 0.1f; // Max distance between npc and distance to have reached it. 
    public int destinationStation; // Station that hase been chosen to go to.
    public int destinationPoint; // Standing point that has been chosen to go to.

    public SellableObject desiredItem; // Item which the npc will buy

    [Header("Values")]
    public int maxStationChange = 3; // Amount of times the npc can look at another station after the previous one.
    public int curStationChange = 0; // Current amount of times the npc changed station.
    public int maxPickCount = 20; // Maximal number of checks the npc can do for an availible station.
    public int curPickCount = 0; // Current number of checks the npc did for an availible station.
    public float maxLookTime = 10; // Maximal time the npc can look at items at a buying station.
    public float minLookTime = 5; // Minimal time the npc should look at items at a buying station.
    public float curLookTime; // Current time the npc has been looking at the items.

    // Start is called before the first frame update
    void Start()
    {
        //Setting values
        manager = WorldManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        curLookTime = Random.Range(minLookTime, maxLookTime);
        destination = transform;

        //Adding all states
        for (int i = 0; i < states.Length; i++)
        {
            AddState(states[i].stateName, states[i].stateScript);
        }

        // Defining start states.
        if (purpose == NpcGoals.LOOKAROUND || purpose == NpcGoals.BUY)
        {
            ChangeState(NpcStates.BUYSTATION);
        } else if (purpose == NpcGoals.REPAIR || purpose == NpcGoals.SELL)
        {
            ChangeState(NpcStates.COUNTER);
        }
    }

    private void Update()
    {
        myPos = new Vector3(transform.position.x, 0, transform.position.z);
        destPos = new Vector3(destination.position.x, 0, destination.position.z);
        switch (currentState)
        {
            case NpcStates.BUYSTATION:
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    // When npc is close enough to the station point, make it look at the items.
                    ChangeState(NpcStates.CHECKING);
                }
                break;
            case NpcStates.CHECKING:
                // Countdown for when npc the stops looking at items.
                curLookTime -= Time.deltaTime;
                if (curLookTime < 0)
                {
                    int choice = Random.Range(0, 2); 
                    manager.buyStations[destinationStation].standingPoints[destinationPoint].isOccupied = false;
                    if (choice == 0 && curStationChange < maxStationChange)
                    {
                        // Pick another station to look at.
                        ChangeState(NpcStates.BUYSTATION);
                        curStationChange++;
                    } else
                    {
                        if (purpose == NpcGoals.BUY)
                        {
                            // Setting the Item which will be bought.
                            BuyStation b = manager.buyStations[destinationStation];
                            desiredItem = b.sellableObjects[Random.Range(0, b.sellableObjects.Length)];

                            // Go to the next state if is done looking around.
                            ChangeState(NpcStates.COUNTER);
                        } else
                        {
                            // Was looking around so will leave.
                            ChangeState(NpcStates.LEAVE);
                        }
                    }
                    curLookTime = Random.Range(minLookTime, maxLookTime);
                }
                break;
            case NpcStates.COUNTER:
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    ChangeState(NpcStates.CHECKOUT);
                }
                break;
            case NpcStates.CHECKOUT:
                break;
            case NpcStates.LEAVE:
                break;
            case NpcStates.TALK:
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (hasInteracted) return;

        if (currentState == NpcStates.CHECKOUT)
        {
            checkoutObj.SetActive(true);
        }
        else
        {
            if (currentState == NpcStates.LEAVE) return;
            ChangeState(NpcStates.TALK);
        }
    }
}
