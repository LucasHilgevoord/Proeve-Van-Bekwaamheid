using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAspects
{
    FRONT,
    FRONT_R_QUARTER,
    RIGHT,
    BACK_R_QUARTER,
    BACK,
    BACK_L_QUARTER,
    LEFT,
    FRONT_L_QUARTER
}

[System.Serializable]
public class PlayerSkeletons
{
    public SkeletonDataAsset front;
    public SkeletonDataAsset frontQuarter;
    public SkeletonDataAsset side;
    public SkeletonDataAsset back;
    public SkeletonDataAsset backQuarter;
}

public class CharacterAnimator : MonoBehaviour
{
    public CharacterAspects currentAnim;

    public SkeletonAnimation body;
    public SkeletonAnimation hair;
    public PlayerSkeletons bodyData;
    public PlayerSkeletons hairData;

    private int animStateLenght;

    private void Start()
    {
        animStateLenght = System.Enum.GetValues(typeof(CharacterAspects)).Length;
    }

    public void ChangeAspect(CharacterAspects state)
    {
        currentAnim = state;
        body.ClearState();
        hair.ClearState();

        switch (currentAnim)
        {
            case CharacterAspects.FRONT:
                SetAspect(bodyData.front, hairData.front, 1);
                break;
            case CharacterAspects.FRONT_R_QUARTER:
                SetAspect(bodyData.frontQuarter, hairData.frontQuarter, 1);
                break;
            case CharacterAspects.RIGHT:
                SetAspect(bodyData.side, hairData.side, 1);
                break;
            case CharacterAspects.BACK_R_QUARTER:
                SetAspect(bodyData.backQuarter, hairData.backQuarter, 1);
                break;
            case CharacterAspects.BACK:
                SetAspect(bodyData.back, hairData.back, 1);
                break;
            case CharacterAspects.BACK_L_QUARTER:
                SetAspect(bodyData.backQuarter, hairData.backQuarter, -1);
                break;
            case CharacterAspects.LEFT:
                SetAspect(bodyData.side, hairData.side, -1);
                break;
            case CharacterAspects.FRONT_L_QUARTER:
                SetAspect(bodyData.frontQuarter, hairData.frontQuarter, -1);
                break;
            default:
                break;
        }
        body.Initialize(true);
        hair.Initialize(true);
        ReloadSkin();
    }

    private void SetAspect(SkeletonDataAsset bodyData, SkeletonDataAsset hairData, float xScale = 1)
    {
        body.skeletonDataAsset = bodyData;
        hair.skeletonDataAsset = hairData;
        body.gameObject.transform.localScale = new Vector2(xScale, 1);
    }

    public void ReloadSkin()
    {
        hair.skeleton.SetSkin(GameManager.Instance.hairSkin);
        body.skeleton.SetSkin(GameManager.Instance.bodySkin);
    }

    public void NextState()
    {
        if ((int)currentAnim == animStateLenght - 1)
            currentAnim = 0;
        else
            currentAnim += 1;

        ChangeAspect(currentAnim);
    }
}
