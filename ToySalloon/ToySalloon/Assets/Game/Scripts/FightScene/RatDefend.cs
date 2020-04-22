using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatDefend : MonoBehaviour
{
    public float totalDamage;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text damageText;

    [SerializeField, Tooltip("10 - 20 voor beste resultaat.")]
    private float speed;

    [SerializeField]
    private GameObject bar;

    private StateManager states;

    private float damageAmount;
    private float sliderSpeed;

    private DamageHandeler damage;

    private void Start()
    {
        sliderSpeed = 1;
        damage = GetComponent<DamageHandeler>();
        states = GetComponent<StateManager>();
    }

    private void OnEnable()
    {
        bar.SetActive(true);
        sliderSpeed = 1;
    }

    private void OnDisable()
    {
        bar.GetComponent<Animator>().SetTrigger("UnEquip");
    }

    private void Update()
    {
        HandleDamage();
        SliderLink();
    }

    private void HandleDamage()
    {
        damageAmount += sliderSpeed * Time.deltaTime * (speed * 10);

        if (damageAmount >= 100)
        {
            sliderSpeed = -1;
        }
        else if (damageAmount <= 0)
        {
            sliderSpeed = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sliderSpeed = 0;
            CalculateDamage();
        }
    }

    private void SliderLink()
    {
        slider.value = damageAmount;
    }

    private void CalculateDamage()
    {
        if(damageAmount <= 50)
        {
            totalDamage = (int)damageAmount * 2;
        }
        else
        {
            for (int i = (int)slider.maxValue / 2; i < (int)slider.maxValue; i++)
            {
                totalDamage = (int)Mathf.Abs((damageAmount - i) * 2);
            }
        }

        damage.DealDamage((int)totalDamage, slider.value);
        StartCoroutine(ShowcaseDamage());
    }

    private IEnumerator ShowcaseDamage()
    {
        damageText.text = "Damage: " + totalDamage;
        yield return new WaitForSeconds(1f);
        states.ChangeBehaviour(StateManager.RatState.ATTACK);
    }
}
