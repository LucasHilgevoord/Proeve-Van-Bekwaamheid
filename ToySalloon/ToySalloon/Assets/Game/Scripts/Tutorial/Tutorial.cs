using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tutorial", menuName = "new Tutorial", order = 0)]
public class Tutorial : ScriptableObject
{
    public string title;

    [TextArea(1,3)]
    public string[] bigMessage;

    [TextArea(1, 3)]
    public string[] smallMessage;
}
