using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum RatStates
{
    SideWalk,
    SideIdle,
    SideDestroyStart,
    SideDestroyLoop,
    FrontWalk,
    FrontIdle,
    QuarterIdle,
    QuarterCry
}

public class RatAnimator : MonoBehaviour
{
    private RatStates currentState;
    [SerializeField] private PlayerSkeletons bodyData;
    [SerializeField] private SkeletonAnimation body;
    private Vector2 bodyScale;

    private void Start()
    {
        bodyScale = body.gameObject.transform.localScale;
    }

    public void SetAnimation(int state)
    {
        currentState = (RatStates)state;

        switch (currentState)
        {
            case RatStates.SideWalk:
                AssignAnimationData("walk", bodyData.side);
                break;
            case RatStates.SideIdle:
                AssignAnimationData("idle", bodyData.side);
                break;
            case RatStates.SideDestroyStart:
                AssignAnimationData("destroy_item_active", bodyData.side);
                break;
            case RatStates.SideDestroyLoop:
                AssignAnimationData("destroy_item_activated", bodyData.side);
                break;
            case RatStates.FrontWalk:
                AssignAnimationData("walk", bodyData.front);
                break;
            case RatStates.FrontIdle:
                AssignAnimationData("idle", bodyData.front);
                break;
            case RatStates.QuarterIdle:
                AssignAnimationData("idle", bodyData.frontQuarter);
                break;
            case RatStates.QuarterCry:
                AssignAnimationData("war_cry", bodyData.frontQuarter);
                break;
            default:
                break;
        }
    }

    private void AssignAnimationData(string animName, SkeletonDataAsset data)
    {
        if (body.skeletonDataAsset != data)
        {
            body.ClearState();
            body.skeletonDataAsset = data;
            body.Initialize(true);
        }
        if(body.AnimationName != animName)
        {
            Debug.Log(data);
            Debug.Log(animName);
            body.AnimationState.SetAnimation(0, animName, true);
        }
    }

    public void SetAspect(int x)
    {
        body.gameObject.transform.localScale = new Vector2(x, bodyScale.y);
    }
}
