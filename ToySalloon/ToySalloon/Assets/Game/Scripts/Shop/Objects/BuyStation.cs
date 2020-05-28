using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStation : MonoBehaviour
{
    public StandingPoint[] standingPoints; // Positions where the customers can stand.

    [SerializeField]
    private bool isPreplaced; // Is the station already in the level.
    public SellableObject[] sellableObjects; // Items that the station sells.

    private void Start()
    {
        if (isPreplaced)
        {
            //Adding the already placed station to a list so NPC's can grab them.
            WorldManager.Instance.buyStations.Add(this);
        }
    }
}
