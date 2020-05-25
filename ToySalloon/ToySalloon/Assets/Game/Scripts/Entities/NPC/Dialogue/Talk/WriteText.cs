﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteText : MonoBehaviour
{
    public delegate void Finish();
    public static event Finish OnFinished;

    [SerializeField, Range(0.02f,0.1f)]
    private float typeSpeed;

    public IEnumerator ShowMessage(string mes, TextMeshProUGUI box)
    {
        box.text = "";
        foreach (char letter in mes.ToCharArray())
        {
            box.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        OnFinished();
    }
}