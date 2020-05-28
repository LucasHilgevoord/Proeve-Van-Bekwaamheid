using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField]
    private Text UIMoneyText;
    private float changeSpeed = 1f;

    private AudioSource audioSrc;
    [SerializeField] private AudioClip cash;

    private void OnEnable()
    {
        CheckoutMenu.OnItemBought += UpdateMoney;
        SkinWindow.OnItemBought += UpdateMoney;
    }
    private void OnDisable()
    {
        CheckoutMenu.OnItemBought -= UpdateMoney;
        SkinWindow.OnItemBought -= UpdateMoney;
    }

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        UIMoneyText.text = GameManager.Instance.storeMoney.ToString();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown("m"))
            {
                UpdateMoney(500);
            }
        }
    }

    private void UpdateMoney(int price)
    {
        audioSrc.PlayOneShot(cash);

        float money = GameManager.Instance.storeMoney;
        GameManager.Instance.storeMoney += price;

        DOTween.To(() => money, f => money = f, money + price, changeSpeed).OnUpdate(() =>
        {
            int m = (int)money;
            UIMoneyText.text = m.ToString();
        }).SetEase(Ease.Linear);
    }
}
