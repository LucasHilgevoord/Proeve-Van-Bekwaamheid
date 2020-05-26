using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.AI;
using DG.Tweening;

public class RatController : MonoBehaviour
{
    public delegate void CaughtRat();
    public static event CaughtRat OnCaught;

    private WorldManager manager;
    internal GameObject player;
    private Animator ratAnim;
    private NavMeshAgent agent;

    [SerializeField] private GameObject clouds;
    private Transform destination;

    private bool isCaught;
    private bool atDest;
    private bool lookFurniture;
    
    // Destination
    private float destDistance = 0.5f;
    private float playerDistance = 1.5f;
    private float speed = 5;
    private Vector3 ratPos;
    private Vector3 desPos;

    private int pickedStation;
    private int pickedPoint;

    // Spine
    [SerializeField] private SkeletonAnimation skel;
    [SerializeField] private RatAnimator anim;
    private float errorMargin = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = WorldManager.Instance;
        RatToWayPoint();
    }

    void Update()
    {
        ratPos = new Vector3(transform.position.x, 0, transform.position.z);
        desPos = new Vector3(destination.position.x, 0, destination.position.z);

        CheckPlayerDistance();
        CheckDestinationDistance();
        ChangeAspect();
    }

    /// <summary>
    /// Change the animation and aspect of the rat spine.
    /// </summary>
    private void ChangeAspect()
    {
        if (!atDest)
        {
            // Rotating the spine to the correct direction.
            Vector3 v = transform.InverseTransformDirection(agent.velocity);
            if (v.x != 0 && v.z != 0)
            {
                if (Mathf.Abs(v.x) < errorMargin && v.z < 0)
                {
                    // Front
                    anim.SetAnimation(4);
                }
                else if (Mathf.Abs(v.z) < errorMargin && v.x < 0)
                {
                    // Left
                    anim.SetAnimation(0);
                }
                else if (Mathf.Abs(v.z) < errorMargin && v.x > 0)
                {
                    // Right
                    anim.SetAnimation(0);
                    anim.SetAspect(-1);
                }
            }
        } else
        {
            // Rotating the spine to the correct way so it's facing the furniture
            // Only the left and right side for now
            if (!lookFurniture)
            {
                Vector2 relativePoint = manager.buyStations[pickedStation].transform.InverseTransformPoint(transform.position);

                if (relativePoint.x > 0)
                    anim.SetAspect(1);
                else
                    anim.SetAspect(-1);

                lookFurniture = true;
            }
        }
    }

    /// <summary>
    /// Check the distance between the player and destination.
    /// </summary>
    private void CheckDestinationDistance()
    {
        if (!atDest && Vector3.Distance(ratPos, desPos) < destDistance)
        {
            atDest = true;
            RatDestroy();
        }
    }

    /// <summary>
    /// Check the distance between the player and object.
    /// Transition to scene when objects are close enough.
    /// </summary>
    private void CheckPlayerDistance()
    {
        if (atDest && !isCaught && Vector3.Distance(transform.position, player.transform.position) < playerDistance)
        {
            isCaught = true;
            OnCaught();
        }
    }

    /// <summary>
    /// Move rat to random station.
    /// </summary>
    private void RatToWayPoint ()
    {
        // Pick random station from list of availible stations
        pickedStation = Random.Range(0, manager.buyStations.Count);
        pickedPoint = 0;
        for (int i = 0; i < manager.buyStations[pickedStation].standingPoints.Length - 1; i++)
        {
            if (!manager.buyStations[pickedStation].standingPoints[i].isOccupied)
            {
                pickedPoint = i;
                manager.buyStations[pickedStation].standingPoints[i].isOccupied = true;
                break;
            }
        }

        destination = manager.buyStations[pickedStation].standingPoints[pickedPoint].standingPoint;
        agent.destination = destination.position;
    }

    /// <summary>
    /// Rat destroy cycle.
    /// </summary>
    private void RatDestroy()
    {
        clouds.SetActive(true);
        anim.SetAnimation(3);
    }
}
