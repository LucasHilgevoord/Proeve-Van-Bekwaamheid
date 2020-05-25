using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private bool isCaught;
    private float distance = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (!isCaught && Vector3.Distance(transform.position, player.position) < distance)
        {
            isCaught = true;
            SceneManager.Instance.FadeToScene(2);
        }
    }
}
