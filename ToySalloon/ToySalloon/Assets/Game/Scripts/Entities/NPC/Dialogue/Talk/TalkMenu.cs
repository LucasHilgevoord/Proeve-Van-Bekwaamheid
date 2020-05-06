using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMenu : UIWindow
{
    [SerializeField]
    private NpcController npc;
    [SerializeField]
    private Animator animator;

    private void Update()
    {
        if (animator.GetBool("shouldClose") && AnimatorIsPlaying())
        {
            npc.currentState = npc.prevState;
            npc.agent.destination = npc.destination.position;
            animator.SetBool("shouldClose", false);
            Debug.Log(npc.currentState.ToString());
            gameObject.SetActive(false);
        }
    }

    public void DisableWindow()
    {
        animator.SetBool("shouldClose", true);
    }

    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.99f;
    }
}
