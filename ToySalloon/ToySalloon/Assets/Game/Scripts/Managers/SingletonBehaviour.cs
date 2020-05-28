using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton that will destroy its gameObject if it detects there was already an instance of this type created
/// </summary>
/// <typeparam name="t"></typeparam>
public class SingletonBehaviour<t> : MonoBehaviour
    where t : Component
{
    public static t Instance;

    internal virtual void Awake()
    {
        if (SingletonBehaviour<t>.Instance == null)
            SingletonBehaviour<t>.Instance = this as t;
        else
            Destroy(this.gameObject);
    }
}
