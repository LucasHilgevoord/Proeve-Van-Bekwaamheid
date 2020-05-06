using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CheckoutAction
{
    public string text;
    public Sprite image;
}

public class CheckoutMenu : UIWindow
{
    public delegate void ItemBought(int price);
    public static event ItemBought OnItemBought;

    [SerializeField]
    private NpcController npc;
    [SerializeField]
    private Animator animator;
    private int repairScene = 3;

    private bool willWait;
    private bool hasBought;

    [Header("Purpose button values")]
    [SerializeField]
    private CheckoutAction[] action;
    [SerializeField]
    private Text actionText; // Text for the purpose button
    [SerializeField]
    private Image actionImage; // Image for the purpose button
    [SerializeField]
    private AudioClip cash;

    [SerializeField]
    private GameObject dialogue;
    private Vector2 repairTextPos = new Vector2(14, -15);
    private float repairTextWidth = 1250;

    [Header("Desired Item values")]
    [SerializeField]
    private GameObject itemWindow;
    [SerializeField]
    private Text itemPrice;
    [SerializeField]
    private Image itemImage;

    private void Start()
    {
        actionText.text = action[(int)npc.purpose].text;
        actionImage.sprite = action[(int)npc.purpose].image;

        if (npc.purpose == NpcGoals.REPAIR)
        {
            itemWindow.SetActive(false);

            //Moving text to the right position.
            RectTransform rt = dialogue.GetComponent<RectTransform>();
            rt.localPosition = repairTextPos;
            rt.sizeDelta = new Vector2(repairTextWidth, rt.sizeDelta.y);
        } else if (npc.purpose == NpcGoals.BUY)
        {
            itemPrice.text = npc.desiredItem.price.ToString();
            itemImage.sprite = npc.desiredItem.itemImage;
        }
    }

    private void Update()
    {
        if (animator.GetBool("shouldClose") && AnimatorIsPlaying())
        {
            gameObject.SetActive(false);
            if (!willWait)
            {
                if (hasBought)
                    OnItemBought(npc.desiredItem.price);
                npc.ChangeState(NpcStates.LEAVE);
            }

            willWait = false;
            animator.SetBool("shouldClose", false);
        }
    }

    /// <summary>
    /// What happens when clicked on the action button
    /// </summary>
    public void OnGoalAction()
    {
        switch (npc.purpose)
        {
            case NpcGoals.BUY:
                npc.audioSrc.PlayOneShot(cash);
                animator.SetBool("shouldClose", true);
                hasBought = true;
                break;
            case NpcGoals.SELL:
                break;
            case NpcGoals.REPAIR:
                npc.manager.FadeToScene(repairScene);
                animator.SetBool("shouldClose", true);
                break;
            default:
                break;
        }
    }

    public void OnWait()
    {
        animator.SetBool("shouldClose", true);
        willWait = true;
    }

    public void OnRefuse()
    {
        animator.SetBool("shouldClose", true);
    }

    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.99f;
    }
}
