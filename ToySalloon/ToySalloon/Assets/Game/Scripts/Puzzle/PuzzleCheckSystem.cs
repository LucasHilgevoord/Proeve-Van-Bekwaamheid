using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheckSystem : MonoBehaviour
{
    public void OnConfirm()
    {
        Application.LoadLevel(1);
    }
}
