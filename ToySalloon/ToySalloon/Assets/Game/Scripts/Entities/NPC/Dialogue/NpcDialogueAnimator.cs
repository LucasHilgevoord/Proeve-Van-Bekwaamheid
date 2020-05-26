using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueAnimator : MonoBehaviour
{
    [SerializeField] private CharacterAnimator characterAnim;
    [SerializeField] private SkeletonGraphic body;
    [SerializeField] private AudioSource audioSrc;

    private string bodySkinName;

    private void OnEnable()
    {
        WriteText.OnFinished += TalkTrigger;
    }

    private void OnDisable()
    {
        WriteText.OnFinished -= TalkTrigger;
        StopTalking(false);
    }

    private void Start()
    {
        bodySkinName = characterAnim.bodySkinName;
        SetSkin();
    }

    private void SetSkin()
    {
        body.Skeleton.SetSkin(bodySkinName);
    }

    private void TalkTrigger()
    {
        StopTalking(true);
    }

    private void StopTalking(bool stop)
    {
        string anim = stop == true ? "idle" : "talk_long";
        body.AnimationState.SetAnimation(0, anim, true);
        audioSrc.mute = stop;
    }
}
