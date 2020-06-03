using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private Transform employeePoint; // Position where the employee stands.
    public StandingPoint[] standingPoints; // Positions where the customers can stand

    [SerializeField]
    private PlayerMovement player;

    private void OnMouseDown()
    {
        player.MovePlayerToPos(new Vector3(employeePoint.position.x, 0.1f, employeePoint.position.z));
    }
}
