using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMenu : UIWindow
{
    [SerializeField]
    private NpcController npc;

    public void DisableWindow()
    {
        npc.currentState = npc.prevState;
        npc.agent.destination = npc.destination.position;
        //Animation
        gameObject.SetActive(false);
    }
}
