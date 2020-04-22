using System;

[Serializable]
public enum NpcGoals
{
    BUY, // Wanting to buy an item.
    REPAIR, // Wanting to get something repaired.
    LOOKAROUND, // Only look around without buying/selling/repairing.
    SELL // Wanting to sell an item.
}
