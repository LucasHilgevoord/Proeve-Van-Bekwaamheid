using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandeler : MonoBehaviour
{
    [SerializeField]
    private RatStats rat;

    [SerializeField]
    private Text damageText;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Animator animatorBar;

    private FightTimeBar bar;

    private void Start()
    {
        bar = GetComponent<FightTimeBar>();
    }

    public void DealDamage(int damage, float sliderValue)
    {
        rat.health -= damage;
        damageText.text = damage.ToString();

        rat.ratAnimator.SetTrigger("TakeDamage");
        cam.GetComponent<Animator>().SetTrigger("Shake");

        #region checking damage quality
        if (CheckDamangeQuality(sliderValue, 28, 36))
        {
            animatorBar.SetTrigger("Blue2");
        }
        else if (CheckDamangeQuality(sliderValue, 37, 45))
        {
            animatorBar.SetTrigger("Purple2");
        }
        else if (CheckDamangeQuality(sliderValue, 46, 54))
        {
            animatorBar.SetTrigger("Yellow");
        }
        else if (CheckDamangeQuality(sliderValue, 55, 63))
        {
            animatorBar.SetTrigger("Purple1");
        }
        else if (CheckDamangeQuality(sliderValue, 64, 71))
        {
            animatorBar.SetTrigger("Blue1");
        }
        #endregion
    }

    private bool CheckDamangeQuality(float value, int min, int max)
    {
        if ((int)value >= min && (int)value <= max)
        {
            return true;
        }
        return false;
    }
}
