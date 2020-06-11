using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Spine.Unity;

public class TalkMenu : UIWindow
{
    [SerializeField] private NpcController npc;
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private WriteText animatedText;

    private bool isOpen = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Start Dialogue animation
        TextMeshProUGUI textMesh = dialogue.GetComponent<TextMeshProUGUI>();
        StartCoroutine(animatedText.ShowMessage(textMesh.text, textMesh));
    }

    public void DisableWindow()
    {
        animator.SetBool("shouldClose", true);
    }

    public void AnimationEnd()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            npc.currentState = npc.prevState;
            npc.agent.destination = npc.destination.position;
            animator.SetBool("shouldClose", false);
            gameObject.SetActive(false);
        }
    }
}
