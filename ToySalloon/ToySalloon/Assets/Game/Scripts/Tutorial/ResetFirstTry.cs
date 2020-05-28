using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFirstTry : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("FirstPlay", 1);
            PlayerPrefs.Save();
        }
    }
}
