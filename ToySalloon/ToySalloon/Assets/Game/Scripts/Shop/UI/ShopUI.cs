using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Animator anim;

    [Header("Audio")]
    private bool soundOn = true;
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
        CheckAudio();
    }

    private void DisableUI(bool isOpen)
    {
        anim.SetBool("shouldClose", isOpen);
    }
    public void OnSoundButton()
    {
        soundOn = soundOn == true ? false : true;
        soundObj.sprite = soundOn == true ? soundSpr[0] : soundSpr[1];

        if (soundOn)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

    private void CheckAudio()
    {
        // Set the audio button sprite to inactive sprite.
        if (AudioListener.volume == 0)
            OnSoundButton();
    }

    public void OnCustomizationButton()
    {
        SceneManager.Instance.FadeToScene(1);
    }

    public void OnMainMenu()
    {
        GameManager.Instance.SaveData();
        SceneManager.Instance.FadeToScene(0);
    }
}
