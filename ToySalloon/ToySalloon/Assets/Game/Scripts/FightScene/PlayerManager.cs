using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int playerHealth;

    public bool damageReady = true;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Text damageText;

    [SerializeField]
    private Animator camAni;

    private void Update()
    {
        healthSlider.value = playerHealth;

        if (playerHealth <= 0)
        {
            camAni.SetTrigger("Fall");
        }
    }

    private void OnEnable()
    {
        damageReady = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Claw")
        {
            if (damageReady)
            {
                StartCoroutine(TakeDamage());
                damageReady = false;
            }
        }
    }

    private IEnumerator TakeDamage()
    {
        playerHealth -= 25;
        damageText.text = 25.ToString();
        GetComponent<Animator>().SetTrigger("Flicker");
        yield return new WaitForSeconds(3f);
        damageReady = true;
    }
}
