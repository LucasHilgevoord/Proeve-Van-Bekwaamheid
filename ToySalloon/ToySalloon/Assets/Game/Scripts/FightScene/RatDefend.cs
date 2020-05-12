using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatDefend : MonoBehaviour
{
    [Header("Damage Info")]
    public float totalDamage;
    [SerializeField]
    private Text damageText;

    [Header("Slider Info")]
    [SerializeField, Range(10f, 20f)]
    private float speed;
    [SerializeField]
    private GameObject bar;
    [SerializeField]
    private Slider slider;

    [Header("Animators")]
    [SerializeField]
    private Animator ratAni;
    [SerializeField]
    private Animator camAni;

    private StateManager states;

    private float damageAmount;
    private float sliderSpeed;

    private DamageHandeler damage;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        sliderSpeed = 1;
        damage = GetComponent<DamageHandeler>();
        states = GetComponent<StateManager>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        bar.SetActive(true);
        sliderSpeed = 1;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        HandleDamage();
        SliderLink();
    }

    /// <summary>
    /// 
    /// </summary>
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
            ShowcaseDamage();
            StartCoroutine(ManageState());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SliderLink()
    {
        slider.value = damageAmount;
    }

    /// <summary>
    /// 
    /// </summary>
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
    }

    /// <summary>
    /// 
    /// </summary>
    private void ShowcaseDamage()
    {
        damageText.text = "Damage: " + totalDamage;
    }

    /// <summary>
    /// 
    /// </summary>
    private IEnumerator ManageState()
    {
        yield return new WaitForSeconds(0.5f);
        bar.GetComponent<Animator>().SetTrigger("UnEquip");
        camAni.SetTrigger("Rage");
        ratAni.SetTrigger("Rage");
        yield return new WaitForSeconds(0.8f);
        states.ChangeBehaviour(StateManager.RatState.ATTACK);
    }
}
