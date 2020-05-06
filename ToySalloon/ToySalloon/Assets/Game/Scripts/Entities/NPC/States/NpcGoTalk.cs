using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoTalk : State
{
    protected override void Start()
    {
        base.Start();
        Talk();
    }

    /// <summary>
    /// Open a dialogue window when clicked on the npc.
    /// </summary>
    private void Talk()
    {
        c.prevState = c.currentState;
        Debug.Log(c.prevState.ToString());
        c.agent.destination = transform.position;
        c.currentState = NpcStates.TALK;
        c.conversationObj.SetActive(true);
    }
}
