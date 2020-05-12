using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("hit");
        }
    }
}
