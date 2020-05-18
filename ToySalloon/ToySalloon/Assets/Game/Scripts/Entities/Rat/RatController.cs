using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private bool isCaught;
    private float distance = 1.5f;

    private Animator ratAnim;

    private void Start()
    {
        ratAnim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateFightScene();
    }

    // Activate the fight scene
    void ActivateFightScene()
    {
        if (!isCaught && Vector3.Distance(transform.position, player.position) < distance)
        {
            isCaught = true;
            WorldManager.SharedInstance.FadeToScene(2);
        }
    }
}
