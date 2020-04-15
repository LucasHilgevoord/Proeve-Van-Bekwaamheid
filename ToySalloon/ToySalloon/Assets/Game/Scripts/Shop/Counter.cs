using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private Transform employeePoint; // Position where the employee stands.
    [SerializeField]
    public StandingPoint[] standingPoint; // Positions where the customers can stand
}
