using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RatState
{
    IDLE,
    WALKING,
    FIGHTING
}

public class RatController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private bool isCaught;
    private float distance = 1.5f;

    public RatState ratState;

    // The waypoints where the rat can go
    public GameObject[] waypoints = new GameObject[0];
    int current;
    public float speed = 5;
    float wPradius = 1;

    private bool isFighting = false;
    private Animator ratAnim;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waypoints = GameObject.FindGameObjectsWithTag("RatWaypoint");
        ratState = RatState.WALKING;
        current = Random.Range(0, 4);

        ratAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (ratState)
        {
            case RatState.IDLE:
                Debug.Log("IDLE");
                break;
            case RatState.WALKING:
                Debug.Log("WALKING");
                RatToWayPoint();
                break;
            case RatState.FIGHTING:
                Debug.Log("FIGHTING");
                RatAttack();
                break;
            default:
                break; 
        }
    }

    // Activate the fight scene
    void ActivateFightScene()
    {
        if (!isCaught && Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            isCaught = true;
            WorldManager.SharedInstance.FadeToScene(2);
            Destroy(gameObject);
        }
    }

    // Move rat to waypoint
    void RatToWayPoint ()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < wPradius)
        {
            ratState = RatState.FIGHTING;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    void RatAttack()
    {
        if (ratState == RatState.FIGHTING)
        {
            ratAnim.SetBool("isFighting", true);
            ActivateFightScene();
        }
    }
}
