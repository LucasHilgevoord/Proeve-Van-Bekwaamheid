using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatStats : MonoBehaviour
{
    public int health;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Animator victoryCanvas;

    public Animator ratAnimator;

    private void Start()
    {
        ratAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        healthSlider.value = health;
    }

    public IEnumerator Flee()
    {
        ratAnimator.SetTrigger("Flee");
        yield return new WaitForSeconds(1f);
        victoryCanvas.gameObject.SetActive(true);
        Debug.Log(":)");
    }
}
