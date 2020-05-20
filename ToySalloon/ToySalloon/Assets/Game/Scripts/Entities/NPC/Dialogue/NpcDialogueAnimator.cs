using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueAnimator : MonoBehaviour
{
    [SerializeField] private CharacterAnimator characterAnim;
    [SerializeField] private SkeletonGraphic body;

    private string bodySkinName;

    private void Start()
    {
        bodySkinName = characterAnim.bodySkinName;
        SetSkin();
    }

    private void SetSkin()
    {
        body.Skeleton.SetSkin(bodySkinName);
    }
}
