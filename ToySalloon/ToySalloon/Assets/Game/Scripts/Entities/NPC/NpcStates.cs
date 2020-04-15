using System;

[Serializable]
public class NpcStates
{
    public enum states
    {
        NONE,
        BUYSTATION, // Walking to a buy station
        CHECKING, // Checking out the buy station
        COUNTER, // Walking to the counter
        CHECKOUT, // Waiting for checkout
        LEAVE, // Walking to the door and leave
        TALK
    }
}
