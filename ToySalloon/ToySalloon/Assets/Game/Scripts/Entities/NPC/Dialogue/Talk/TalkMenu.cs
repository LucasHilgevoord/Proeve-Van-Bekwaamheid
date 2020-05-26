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

    private void OnEnable()
    {
        // Start Dialogue animation
        TextMeshProUGUI textMesh = dialogue.GetComponent<TextMeshProUGUI>();
        StartCoroutine(animatedText.ShowMessage(textMesh.text, textMesh));
    }

    private void Update()
    {
        if (animator.GetBool("shouldClose") && AnimatorIsPlaying())
        {
            npc.currentState = npc.prevState;
            npc.agent.destination = npc.destination.position;
            animator.SetBool("shouldClose", false);
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
