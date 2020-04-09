using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIcon : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float distance = 1.2f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.position));
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            gameObject.SetActive(false);
        }
    }
}
