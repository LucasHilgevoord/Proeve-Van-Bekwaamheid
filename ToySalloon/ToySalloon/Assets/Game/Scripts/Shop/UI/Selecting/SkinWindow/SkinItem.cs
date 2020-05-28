using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    public delegate void SkinSelected(SkinItem item);
    public static event SkinSelected OnSkinSelect;

    public int id;
    [SerializeField] private Button btn;

    [SerializeField] private GameObject levelLock;
    [SerializeField] private Text levelText;

    [SerializeField] private GameObject moneyLock;
    [SerializeField] private Text moneyText;

    [SerializeField] private GameObject selectedIcon;
    [SerializeField] private Image image;

    public void SetImage(Sprite icon)
    {
        image.sprite = icon;
    }

    public void OnButtonClick()
    {
        OnSkinSelect(this);
    }

    public void SetSelected(bool value)
    {
        selectedIcon.SetActive(value);
        btn.interactable = !value;
    }

    public void SetLevelLock(int level)
    {
        btn.interactable = false;
        levelLock.SetActive(true);
        levelText.text = level.ToString();
    }

    public void SetMoneyLock(int price)
    {
        moneyLock.SetActive(true);
        moneyText.text = price.ToString();
    }

    public void DisableMoney()
    {
        moneyLock.SetActive(false);
    }
}
