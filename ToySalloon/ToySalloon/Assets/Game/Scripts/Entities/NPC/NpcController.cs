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
    public NpcGoals.goals purpose; // Purpose of the npc that is entering the store.
    public NpcStates.states currentState; // Current state the npc is in.
    public NpcStates.states prevState;
    public StateID[] states = {
        new StateID(NpcStates.states.BUYSTATION, typeof(NpcGoStation)),
        new StateID(NpcStates.states.CHECKING, typeof(NpcGoCheck)),
        new StateID(NpcStates.states.COUNTER, typeof(NpcGoCounter)),
        new StateID(NpcStates.states.CHECKOUT, typeof(NpcGoCheckout)),
        new StateID(NpcStates.states.LEAVE, typeof(NpcGoLeave)),
        new StateID(NpcStates.states.TALK, typeof(NpcGoTalk)),
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
        manager = WorldManager.SharedInstance;
        agent = GetComponent<NavMeshAgent>();
        curLookTime = Random.Range(minLookTime, maxLookTime);
        destination = transform;

        //Testing!!!
        purpose = NpcGoals.goals.BUY;

        //Adding all states
        for (int i = 0; i < states.Length; i++)
        {
            AddState(states[i].stateName, states[i].stateScript);
        }

        //Defining start states.
        if (purpose == NpcGoals.goals.LOOKAROUND || purpose == NpcGoals.goals.BUY)
        {
            ChangeState(NpcStates.states.BUYSTATION);
        } else if (purpose == NpcGoals.goals.REPAIR || purpose == NpcGoals.goals.SELL)
        {
            ChangeState(NpcStates.states.COUNTER);
        }
    }

    private void Update()
    {
        myPos = new Vector3(transform.position.x, 0, transform.position.z);
        destPos = new Vector3(destination.position.x, 0, destination.position.z);
        switch (currentState)
        {
            case NpcStates.states.BUYSTATION:
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    //When npc is close enough to the station point, make it look at the items.
                    ChangeState(NpcStates.states.CHECKING);
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
                        ChangeState(NpcStates.states.BUYSTATION);
                        curStationChange++;
                    } else
                    {
                        //Go to the next state if is done looking around
                        if (purpose == NpcGoals.goals.BUY)
                        {
                            ChangeState(NpcStates.states.COUNTER);
                        } else
                        {
                            ChangeState(NpcStates.states.LEAVE);
                        }
                    }
                    curLookTime = Random.Range(minLookTime, maxLookTime);
                }
                break;
            case NpcStates.states.COUNTER:
                if (Vector3.Distance(myPos, destPos) < destinationRange)
                {
                    ChangeState(NpcStates.states.CHECKOUT);
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
            ChangeState(NpcStates.states.TALK);
        }
    }
}
