using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region SingleTon
    private static WorldManager instance = null;
    public static WorldManager SharedInstance {
        get {
            if (instance == null)
            {
                instance = new WorldManager();
            }
            return instance;
        }
    }
    #endregion

    [Header("Shop Settings")]
    public int maxCustomers = 5; // Max customers allowed in the store.

    [Header("Shop Objects")]
    public List<BuyStation> buyStations = new List<BuyStation>(); // all buy stations in the store.
    public List<NpcController> npcs = new List<NpcController>(); // all NPC's that are currently in the store.
    public Counter[] counters; // All counters in the store.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
