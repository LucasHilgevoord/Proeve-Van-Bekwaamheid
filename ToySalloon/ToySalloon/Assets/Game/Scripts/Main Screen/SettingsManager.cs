using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Toggle buttons
    [SerializeField]
    private GameObject fullScreenText;
    [SerializeField]
    private GameObject shadowOffText;
    [SerializeField]
    private GameObject shadowOffTextLine;
    [SerializeField]
    private GameObject shadowLowText;
    [SerializeField]
    private GameObject shadowLowTextLine;
    [SerializeField]
    private GameObject shadowHighText;
    [SerializeField]
    private GameObject shadowHighTextLine;
    [SerializeField]
    private GameObject shadowHudText;
    [SerializeField]
    private GameObject cameraEffectsText;
    [SerializeField]
    private GameObject invertMouseText;
    [SerializeField]
    private GameObject vsyncText;
    [SerializeField]
    private GameObject motionBlurText;
    [SerializeField]
    private GameObject amvientOcclusionText;
    [SerializeField]
    private GameObject textureLowText;
    [SerializeField]
    private GameObject textureLowTextLine;
    [SerializeField]
    private GameObject textureMedText;
    [SerializeField]
    private GameObject textureMedTextLine;
    [SerializeField]
    private GameObject textureHighText;
    [SerializeField]
    private GameObject textureHighTextLine;
    [SerializeField]
    private GameObject aaoffText;
    [SerializeField]
    private GameObject aaoffTextLine;
    [SerializeField]
    private GameObject aa2xText;
    [SerializeField]
    private GameObject aa2xTextLine;
    [SerializeField]
    private GameObject aa4xText;
    [SerializeField]
    private GameObject aa4xTextLine;
    [SerializeField]
    private GameObject aa8xText;
    [SerializeField]
    private GameObject aa8xTextLine;

    // Sliders
    [SerializeField]
    private GameObject musicSlider;
    [SerializeField]
    private GameObject sfxSlider;
    [SerializeField]
    private GameObject sensitivityXSlider;
    [SerializeField]
    private GameObject sensitivityYSlider;
    [SerializeField]
    private GameObject mouseSmoothSlider;

    private float sliderValue = 0.0f;
    private float sliderValueSFX = 0.0f;
    private float sliderValueXSensitivity = 0.0f;
    private float sliderValueYSensitivity = 0.0f;
    private float sliderValueSmoothing = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
