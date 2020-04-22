using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentAvoider : MonoBehaviour
{
    private NavMeshAgent otherAgent;
    private NavMeshAgent myAgent;

    private NavMeshObstacle otherObstacle;
    private NavMeshObstacle myObstacle;

    private GameObject collidedObj;

    private bool isActive = false;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myObstacle = GetComponent<NavMeshObstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Entity")
        {
            collidedObj = other.gameObject;
            otherAgent = collidedObj.GetComponent<NavMeshAgent>();
            if (otherAgent.avoidancePriority < myAgent.avoidancePriority)
            {
                // Other agent will stop
                otherObstacle = collidedObj.GetComponent<NavMeshObstacle>();
                otherAgent.velocity = Vector3.zero;
                otherAgent.enabled = false;
                otherObstacle.enabled = true;
            } else
            {
                // My agent will stop
                myAgent.velocity = Vector3.zero;
                myAgent.enabled = false;
                myObstacle.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Entity")
        {
            collidedObj = other.gameObject;
            otherAgent = collidedObj.GetComponent<NavMeshAgent>();
            if (otherAgent.avoidancePriority < myAgent.avoidancePriority)
            {
                // Other agent will restart nav
                otherObstacle = collidedObj.GetComponent<NavMeshObstacle>();
                otherObstacle.enabled = false;
                otherAgent.enabled = true;
            }
            else
            {
                // My agent will restart
                otherObstacle = collidedObj.GetComponent<NavMeshObstacle>();
                myObstacle.enabled = false;
                myAgent.enabled = true;
            }
        }
    }
}
