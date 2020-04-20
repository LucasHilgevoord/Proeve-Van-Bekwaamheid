using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightTimeBar : MonoBehaviour
{
    public float totalDamage;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text damageText;

    [SerializeField, Tooltip("10 - 20 voor beste resultaat.")]
    private float speed;

    private float damageAmount;
    private float sliderSpeed;

    private DamageHandeler damage;

    private void Start()
    {
        sliderSpeed = 1;
        damage = GetComponent<DamageHandeler>();
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

        //temp (resets the slider)
        if(Input.GetKeyDown(KeyCode.R))
        {
            sliderSpeed = 1;
            damageText.text = "Damage: ";
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
        ShowcaseDamage();
    }

    private void ShowcaseDamage()
    {
        damageText.text = "Damage: " + totalDamage;
    }
}
