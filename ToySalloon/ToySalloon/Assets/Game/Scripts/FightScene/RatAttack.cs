using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttack : MonoBehaviour
{
    public float clawSpeed;

    [SerializeField]
    private float spawnCooldown = 1;

    [SerializeField]
    private GameObject claw;

    [SerializeField]
    private Transform clawParent;

    private StateManager states;

    private float xpos;

    private void OnEnable()
    {
        StartCoroutine(SpawnTimer());
        clawParent.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    private void OnDisable()
    {
        clawParent.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    private void Start()
    {
        states = GetComponent<StateManager>();
    }

    private IEnumerator SpawnTimer()
    {
        int num = 25;
        while (num > 1)
        {
            yield return new WaitForSeconds(spawnCooldown);
            SpawnClaws();
            num -= 1;
        }
        yield return new WaitForSeconds(2.5f);
        states.ChangeBehaviour(StateManager.RatState.TAKEDAMAGE);
    }

    private void SpawnClaws()
    {
        xpos = Random.Range(-6.0f, 6.0f);

        Instantiate(claw, new Vector3(xpos, 7, clawParent.position.z), Quaternion.identity, clawParent);
    }
}
