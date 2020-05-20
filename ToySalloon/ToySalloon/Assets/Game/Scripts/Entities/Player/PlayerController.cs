using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animation playerAnim;
    public RatController ratObj;
    private bool isAnimating = false;

    void Start()
    {
        playerAnim = GetComponent<Animation>();
    }

    void Update()
    {
        PlayerSearchRats();
    }

    void PlayerSearchRats()
    {
        if (GameObject.FindObjectOfType<RatController>())
        {
            ratObj = FindObjectOfType<RatController>();
        }
        if (ratObj && ratObj.ratState == RatState.FIGHTING)
        {
            playerAnim.Play("PlayerShocked");
        }
    }
}
