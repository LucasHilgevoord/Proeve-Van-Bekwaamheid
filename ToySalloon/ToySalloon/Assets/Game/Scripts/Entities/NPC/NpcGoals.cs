using System;

[Serializable]
public class NpcGoals
{
    public enum goals
    {
        LOOKAROUND, // Only look around without buying/selling/repairing.
        BUY, // Wanting to buy an item.
        SELL, // Wanting to sell an item.
        REPAIR // Wanting to get something repaired.
    }
}
