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
    private bool hasAttacked;

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

    [Header("AudioSources")]
    [SerializeField]
    private PlayAudio audio;
    [SerializeField]
    private AudioClip hit;

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
        hasAttacked = false;
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

        if (Input.GetKeyDown(KeyCode.Space) && !hasAttacked)
        {
            sliderSpeed = 0;

            CalculateDamage();
            ShowcaseDamage();
            StartCoroutine(ManageState());
            hasAttacked = true;
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
    }

    private void ShowcaseDamage()
    {
        damageText.text = "Damage: " + totalDamage;
    }

    private IEnumerator ManageState()
    {
        audio.Play(hit);
        yield return new WaitForSeconds(0.5f);
        bar.GetComponent<Animator>().SetTrigger("UnEquip");
        if (ratAni.GetComponent<RatStats>().health <= 0)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(ratAni.GetComponent<RatStats>().Flee());
            states.ChangeBehaviour(StateManager.RatState.IDLE);
            yield break;
        }
        camAni.SetTrigger("Rage");
        ratAni.SetTrigger("Rage");
        yield return new WaitForSeconds(1.8f);
        states.ChangeBehaviour(StateManager.RatState.ATTACK);
    }
}
