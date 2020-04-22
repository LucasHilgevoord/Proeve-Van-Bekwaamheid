using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public NpcController c;

    protected virtual void Start()
    {
        c = this.gameObject.GetComponent<NpcController>();
    }
}
