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

    private void OnEnable()
    {
        CheckoutMenu.OnItemBought += UpdateMoney;
    }
    private void OnDisable()
    {
        CheckoutMenu.OnItemBought -= UpdateMoney;
    }

    private void UpdateMoney(int price)
    {
        Debug.Log("Money is updated");
        float money = GameManager.SharedInstance.storeMoney;
        Debug.Log(money);
        Debug.Log(price);

        DOTween.To(() => money, f => money = f, money + price, changeSpeed).OnUpdate(() =>
        {
            int m = (int)money;
            UIMoneyText.text = m.ToString();
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameManager.SharedInstance.storeMoney += price;
        });
    }
}
