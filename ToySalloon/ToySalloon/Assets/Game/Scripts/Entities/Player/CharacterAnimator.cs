using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private NavMeshAgent nav;

    public SkeletonAnimation body;
    public SkeletonAnimation hair;
    public PlayerSkeletons bodyData;
    public PlayerSkeletons hairData;

    private int animStateLenght;
    private float errorMargin = 1f;
    private Vector3 bodyScale;

    internal string bodySkinName;

    protected virtual void Start()
    {
        animStateLenght = System.Enum.GetValues(typeof(CharacterAspects)).Length;
        ReloadSkin();

        nav = GetComponent<NavMeshAgent>();
        bodyScale = body.gameObject.transform.localScale;
    }

    private void Update()
    {
        if (nav)
            UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector3 v = transform.InverseTransformDirection(nav.velocity);
        if (v.x == 0 && v.z == 0)
        {
            CheckNewAspect(CharacterAspects.FRONT);
            SetAnimation("idle");
        }
        else
        {
            if (Mathf.Abs(v.x) < errorMargin && v.z > 0)
            {
                CheckNewAspect(CharacterAspects.BACK);
            }
            else if (Mathf.Abs(v.x) < errorMargin && v.z < 0)
            {
                CheckNewAspect(CharacterAspects.FRONT);
            }
            else if (Mathf.Abs(v.z) < errorMargin && v.x < 0)
            {
                CheckNewAspect(CharacterAspects.LEFT);
            }
            else if (Mathf.Abs(v.z) < errorMargin && v.x > 0)
            {
                CheckNewAspect(CharacterAspects.RIGHT);
            }
            else if (v.z > errorMargin && v.x < errorMargin)
            {
                CheckNewAspect(CharacterAspects.BACK_L_QUARTER);
            }
            else if (v.z < errorMargin && v.x > errorMargin)
            {
                CheckNewAspect(CharacterAspects.FRONT_R_QUARTER);
            }
            else if (v.z > errorMargin && v.x > errorMargin)
            {
                CheckNewAspect(CharacterAspects.BACK_R_QUARTER);
            }
            else if (v.z < errorMargin && v.x < errorMargin)
            {
                CheckNewAspect(CharacterAspects.FRONT_L_QUARTER);
            }
            SetAnimation("walk");
        }

    }

    private void CheckNewAspect(CharacterAspects newAspect)
    {
        if (newAspect == currentAnim) return;
        ChangeAspect(newAspect);
    }

    private void SetAnimation(string animationName)
    {
        if (body.AnimationName != animationName)
            body.AnimationState.SetAnimation(0, animationName, true);

        if (hair != null && hair.AnimationName != animationName)
            hair.AnimationState.SetAnimation(0, animationName, true);
    }

    public void ChangeAspect(CharacterAspects state)
    {
        currentAnim = state;
        body.ClearState();

        if (hair != null) hair.ClearState();

        switch (currentAnim)
        {
            case CharacterAspects.FRONT:
                SetAspect(bodyData.front, hairData.front, false);
                break;
            case CharacterAspects.FRONT_R_QUARTER:
                SetAspect(bodyData.frontQuarter, hairData.frontQuarter, false);
                break;
            case CharacterAspects.RIGHT:
                SetAspect(bodyData.side, hairData.side, false);
                break;
            case CharacterAspects.BACK_R_QUARTER:
                SetAspect(bodyData.backQuarter, hairData.backQuarter, false);
                break;
            case CharacterAspects.BACK:
                SetAspect(bodyData.back, hairData.back, false);
                break;
            case CharacterAspects.BACK_L_QUARTER:
                SetAspect(bodyData.backQuarter, hairData.backQuarter, true);
                break;
            case CharacterAspects.LEFT:
                SetAspect(bodyData.side, hairData.side, true);
                break;
            case CharacterAspects.FRONT_L_QUARTER:
                SetAspect(bodyData.frontQuarter, hairData.frontQuarter, true);
                break;
            default:
                break;
        }
        body.Initialize(true);
        if (hair != null) hair.Initialize(true);
        ReloadSkin();
    }

    private void SetAspect(SkeletonDataAsset bodyData, SkeletonDataAsset hairData, bool flipX)
    {
        body.skeletonDataAsset = bodyData;
        if (hair != null) hair.skeletonDataAsset = hairData;

        float xScale = flipX == true ? -bodyScale.x : bodyScale.x;
        body.gameObject.transform.localScale = new Vector2(xScale, bodyScale.y);
    }

    public void ReloadSkin()
    {
        body.skeleton.SetSkin(bodySkinName);
        if (hair != null) hair.skeleton.SetSkin(GameManager.Instance.hairSkin);
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
