using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    private RatAttack attack;

    private void Start()
    {
        attack = FindObjectOfType<RatAttack>();
    }

    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * attack.clawSpeed);

        if(transform.position.y <= -3.5f)
        {
            Destroy(gameObject);
        }
    }
}
