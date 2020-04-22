using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            Application.LoadLevel(1);
        }
    }
}
