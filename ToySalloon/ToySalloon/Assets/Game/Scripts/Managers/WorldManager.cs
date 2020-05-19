using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonBehaviour<WorldManager>
{
    [Header("Shop Settings")]
    public int maxCustomers = 5; // Max customers allowed in the store.

    [Header("Shop Objects")]
    public List<BuyStation> buyStations = new List<BuyStation>(); // all buy stations in the store.
    public List<NpcController> npcs = new List<NpcController>(); // all NPC's that are currently in the store.
    public Counter counters; // Counter object in the store.
    public Transform door; // Point where npc's leave
}
