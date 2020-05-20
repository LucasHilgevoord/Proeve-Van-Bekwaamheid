using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : CharacterAnimator
{
    protected override void Start()
    {
        bodySkinName = GameManager.Instance.bodySkin;
        base.Start();
    }
}
