using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIcon : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float distance = 1.3f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            gameObject.SetActive(false);
        }
    }
}
