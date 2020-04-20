using System;
using UnityEngine;

[Serializable]
public class StandingPoint
{
    public Transform standingPoint; // Positions of the predefined places where npc's can stand.
    public bool isOccupied; // Is the standing place occupied by a NPC?
}
