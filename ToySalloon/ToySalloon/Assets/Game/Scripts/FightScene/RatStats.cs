using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatStats : MonoBehaviour
{
    public int health;

    public Animator ratAnimator;

    private void Start()
    {
        ratAnimator = GetComponent<Animator>();
    }
}
