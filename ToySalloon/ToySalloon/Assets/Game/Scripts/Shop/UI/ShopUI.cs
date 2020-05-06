using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Animator anim;

    [Header("Audio")]
    private bool soundOn = true;
    private AudioListener audioListener;
    [SerializeField] private Sprite[] soundSpr;
    [SerializeField] private Image soundObj;

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
        anim = GetComponent<Animator>();
        audioListener = Camera.main.GetComponent<AudioListener>();
    }

    private void DisableUI(bool isOpen)
    {
        anim.SetBool("shouldClose", isOpen);
    }

    public void OnMoreButton()
    {

    }

    public void OnSoundButton()
    {
        soundOn = soundOn == true ? false : true;
        soundObj.sprite = soundOn == true ? soundSpr[0] : soundSpr[1];
        audioListener.enabled = soundOn;
    }

    public void OnStoreButton()
    {
        
    }

    public void OnCustomizationButton()
    {
        WorldManager.SharedInstance.FadeToScene(0);
    }
}
