using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum RatState { ATTACK, TAKEDAMAGE, IDLE}
    public RatState state;

    private RatDefend takeDamage;
    private RatAttack dealDamage;

    private void Start()
    {
        takeDamage = GetComponent<RatDefend>();
        dealDamage = GetComponent<RatAttack>();

        ChangeBehaviour(RatState.IDLE);

        StartCoroutine(StartStates());
    }

    private IEnumerator StartStates()
    {
        yield return new WaitForSeconds(9.5f);
        ChangeBehaviour(RatState.TAKEDAMAGE);
    }

    public void ChangeBehaviour(RatState newState)
    {
        state = newState;

        switch (state)
        {
            case RatState.ATTACK:
                Attack();
                break;
            case RatState.TAKEDAMAGE:
                Defend();
                break;
            case RatState.IDLE:
                Idle();
                break;
        }
    }

    private void Attack()
    {
        takeDamage.enabled = false;
        dealDamage.enabled = true;
    }

    private void Defend()
    {
        takeDamage.enabled = true;
        dealDamage.enabled = false;
    }

    private void Idle()
    {
        takeDamage.enabled = false;
        dealDamage.enabled = false;
    }
}
