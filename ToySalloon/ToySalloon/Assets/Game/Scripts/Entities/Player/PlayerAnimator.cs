using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : CharacterAnimator
{
    protected override void ReloadSkin()
    {
        // Assigning the skin of the gamemanager.
        body.skeleton.SetSkin(GameManager.Instance.bodySkin);
        if (hair != null) hair.skeleton.SetSkin(GameManager.Instance.hairSkin);
    }
}
