using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAudio : StateMachineBehaviour
{
    [SerializeField]
    private AudioClip screech;
    [SerializeField]
    private AudioSource source;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source.PlayOneShot(screech);
    }
}
