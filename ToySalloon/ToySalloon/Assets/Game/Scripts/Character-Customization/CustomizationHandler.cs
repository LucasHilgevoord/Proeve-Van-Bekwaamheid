using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationHandler : MonoBehaviour
{
    [SerializeField]
    private Customization custom;

    [SerializeField]
    private ClosetManager closet;

    private void Start()
    {
        //temp (alleen voor de kleuren)
        gameObject.transform.GetChild(0).GetComponent<Image>().color = custom.color;
    }

    public void ChangeSprite()
    {
        closet.characterPart.sprite = custom.customizable;

        //Temp (alleen voor de kleuren)
        closet.characterPart.color = custom.color;
    }
}
