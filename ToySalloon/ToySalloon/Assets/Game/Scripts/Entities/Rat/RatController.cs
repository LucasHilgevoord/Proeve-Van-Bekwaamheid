using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private bool isCaught;
    private float distance = 1.5f;

    private Animator ratAnim;

    private void Start()
    {
        ratAnim = transform.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ActivateFightScene();
    }

    // Activate the fight scene
    void ActivateFightScene()
    {
        if (!isCaught && Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            isCaught = true;
            WorldManager.SharedInstance.FadeToScene(2);
        }
    }
}
