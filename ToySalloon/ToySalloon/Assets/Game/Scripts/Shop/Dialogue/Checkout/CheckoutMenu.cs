using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Purpose button values")]
    [SerializeField]
    private CheckoutAction[] action;
    [SerializeField]
    private Text actionText; // Text for the purpose button
    [SerializeField]
    private Image actionImage; // Image for the purpose button
    [SerializeField]
    private AudioClip cash;

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
        } else if (npc.purpose == NpcGoals.BUY)
        {
            itemPrice.text = "$" + npc.desiredItem.price.ToString();
            itemImage.sprite = npc.desiredItem.itemImage;
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
                OnItemBought(npc.desiredItem.price);
                npc.ChangeState(NpcStates.LEAVE);
                npc.audioSrc.PlayOneShot(cash);
                DisableWindow();
                break;
            case NpcGoals.SELL:
                break;
            case NpcGoals.REPAIR:
                npc.manager.FadeToScene(3);
                break;
            default:
                break;
        }
    }

    public void OnWait()
    {
        DisableWindow();
    }

    public void OnRefuse()
    {
        DisableWindow();
        npc.ChangeState(NpcStates.LEAVE);
    }

    private void DisableWindow()
    {
        gameObject.SetActive(false);
    }
}
