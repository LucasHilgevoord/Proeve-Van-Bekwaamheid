using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private Animation anim;

    private void OnEnable()
    {
        UIWindow.OnWindowOverlay += DisableUI;
    }
    private void OnDisable()
    {
        UIWindow.OnWindowOverlay -= DisableUI;
    }

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void DisableUI(bool isOpen)
    {
        string name = isOpen ? "StoreUIOff" : "StoreUIOn";
        anim.Play(name);
    }
}
