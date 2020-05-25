using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "New Puzzle", order = 1)]
public class InfoPuzzle : ScriptableObject
{
    public string puzzleName;

    public float coinReward;
    public float expReward;

    public Transform model;

    [TextArea(1, 3)]
    public string tip;

    [TextArea(1,2)]
    public string[] lines;

}
