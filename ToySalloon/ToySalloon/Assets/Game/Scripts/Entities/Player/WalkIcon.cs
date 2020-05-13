using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIcon : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float distance = 0.1f;

    private Vector3 playerPos;
    private Vector3 iconPos;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        playerPos = new Vector3(player.position.x, 0, player.position.z);
        iconPos = new Vector3(transform.position.x, 0, transform.position.z);
        if (Vector3.Distance(iconPos, playerPos) < distance)
        {
            gameObject.SetActive(false);
        }
    }
}
