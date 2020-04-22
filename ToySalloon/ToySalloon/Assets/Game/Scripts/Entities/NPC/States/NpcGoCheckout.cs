using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoCheckout : State
{
    protected override void Start()
    {
        base.Start();
        Checkout();
    }

    /// <summary>
    /// Make the npc wait until the player clicked on him/her.
    /// Opens the checkout window after that.
    /// </summary>
    private void Checkout()
    {
        c.currentState = NpcStates.CHECKOUT;
        c.checkoutIconObj.gameObject.SetActive(true);
        c.checkoutIconObj.sprite = c.checkoutIcon[(int)c.purpose];
    }
}
