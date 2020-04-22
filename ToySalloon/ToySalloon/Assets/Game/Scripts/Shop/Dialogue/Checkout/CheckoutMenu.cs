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

    [Header("Desired Item values")]
    [SerializeField]
    private GameObject itemWindow;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemPrice;
    [SerializeField]
    private Image itemImage;

    private void Start()
    {
        actionText.text = action[(int)npc.purpose].text;
        actionImage.sprite = action[(int)npc.purpose].image;

        if (npc.purpose == NpcGoals.goals.REPAIR)
        {
            itemWindow.SetActive(false);
        } else if (npc.purpose == NpcGoals.goals.BUY)
        {
            itemName.text = npc.desiredItem.itemName;
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
            case NpcGoals.goals.BUY:
                DisableWindow();
                OnItemBought(npc.desiredItem.price);
                Debug.Log(npc.desiredItem.price);

                npc.ChangeState(NpcStates.states.LEAVE);
                break;
            case NpcGoals.goals.SELL:
                break;
            case NpcGoals.goals.REPAIR:
                
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
        npc.ChangeState(NpcStates.states.LEAVE);
    }

    private void DisableWindow()
    {
        gameObject.SetActive(false);
    }
}
