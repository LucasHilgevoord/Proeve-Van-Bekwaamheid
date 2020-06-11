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
    private GameObject shadowOffTextHover;
    [SerializeField]
    private GameObject shadowLowText;
    [SerializeField]
    private GameObject shadowLowTextHover;
    [SerializeField]
    private GameObject shadowHighText;
    [SerializeField]
    private GameObject shadowHighTextHover;
    [SerializeField]
    private GameObject showHudText;
    [SerializeField]
    private GameObject cameraEffectsText;
    [SerializeField]
    private GameObject invertMouseText;
    [SerializeField]
    private GameObject vsyncText;
    [SerializeField]
    private GameObject motionBlurText;
    [SerializeField]
    private GameObject ambientOcclusionText;
    [SerializeField]
    private GameObject textureLowText;
    [SerializeField]
    private GameObject textureLowTextHover;
    [SerializeField]
    private GameObject textureMedText;
    [SerializeField]
    private GameObject textureMedTextHover;
    [SerializeField]
    private GameObject textureHighText;
    [SerializeField]
    private GameObject textureHighTextHover;
    [SerializeField]
    private GameObject aaoffText;
    [SerializeField]
    private GameObject aaoffTextHover;
    [SerializeField]
    private GameObject aa2xText;
    [SerializeField]
    private GameObject aa2xTextHover;
    [SerializeField]
    private GameObject aa4xText;
    [SerializeField]
    private GameObject aa4xTextHover;
    [SerializeField]
    private GameObject aa8xText;
    [SerializeField]
    private GameObject aa8xTextHover;

    // Sliders
    [SerializeField]
    private GameObject musicSlider;
    [SerializeField]
    private GameObject sfxSlider;

    private float sliderValue = 0.0f;
    private float sliderValueSFX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");

        // Fullscreen check
        if (Screen.fullScreen)
        {
            fullScreenText.GetComponent<Text>().text = "on";
        } 
        else if (!Screen.fullScreen)
        {
            fullScreenText.GetComponent<Text>().text = "off";
        }

        // Check hud
        if (PlayerPrefs.GetInt("ShowHUD") == 0)
        {
            showHudText.GetComponent<Text>().text = "off";
        } 
        else
        {
            showHudText.GetComponent<Text>().text = "on";
        }

        // Check Shadow distance and enabled values
        if (PlayerPrefs.GetInt("Shadows") == 0)
        {
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            shadowOffText.GetComponent<Text>().text = "OFF";
            shadowLowText.GetComponent<Text>().text = "low";
            shadowHighText.GetComponent<Text>().text = "high";

            shadowOffTextHover.gameObject.SetActive(true);
            shadowLowTextHover.gameObject.SetActive(false);
            shadowHighTextHover.gameObject.SetActive(false);
        } 
        else if (PlayerPrefs.GetInt("Shadows") == 1)
        {
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            shadowOffText.GetComponent<Text>().text = "OFF";
            shadowLowText.GetComponent<Text>().text = "low";
            shadowHighText.GetComponent<Text>().text = "high";

            shadowOffTextHover.gameObject.SetActive(false);
            shadowLowTextHover.gameObject.SetActive(true);
            shadowHighTextHover.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Shadows") == 2)
        {
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 400;
            shadowOffText.GetComponent<Text>().text = "off";
            shadowLowText.GetComponent<Text>().text = "low";
            shadowHighText.GetComponent<Text>().text = "HIGH";

            shadowOffTextHover.gameObject.SetActive(false);
            shadowLowTextHover.gameObject.SetActive(false);
            shadowHighTextHover.gameObject.SetActive(true);
        }

        // Check vertical sync
        if (QualitySettings.vSyncCount == 0)
        {
            vsyncText.GetComponent<Text>().text = "off";
        } 
        else if (QualitySettings.vSyncCount == 1)
        {
            vsyncText.GetComponent<Text>().text = "on";
        }

        // Check if mouse inver
        if (PlayerPrefs.GetInt("Inverted") == 0)
        {
            invertMouseText.GetComponent<Text>().text = "off";
        } 
        else if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            invertMouseText.GetComponent<Text>().text = "on";
        }

        // Check motion blur
        if (PlayerPrefs.GetInt("MotionBlur") == 0)
        {
            motionBlurText.GetComponent<Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("MotionBlur") == 1)
        {
            motionBlurText.GetComponent<Text>().text = "on";
        }

        // Check ambient occlusion
        if (PlayerPrefs.GetInt("AmbientOcclusion") == 0)
        {
            ambientOcclusionText.GetComponent<Text>().text = "off";
        }
        else if (PlayerPrefs.GetInt("AmbientOcclusion") == 1)
        {
            ambientOcclusionText.GetComponent<Text>().text = "on";
        }

        // Texture quality check
        if (PlayerPrefs.GetInt("Textures") == 0)
        {
            QualitySettings.masterTextureLimit = 2;
            textureLowText.GetComponent<Text>().text = "LOW";
            textureMedText.GetComponent<Text>().text = "med";
            textureHighText.GetComponent<Text>().text = "high";

            textureLowTextHover.gameObject.SetActive(true);
            textureMedTextHover.gameObject.SetActive(false);
            textureHighTextHover.gameObject.SetActive(false);
        } 
        else if (PlayerPrefs.GetInt("Textures") == 1)
        {
            QualitySettings.masterTextureLimit = 1;
            textureLowText.GetComponent<Text>().text = "low";
            textureMedText.GetComponent<Text>().text = "MED";
            textureHighText.GetComponent<Text>().text = "high";

            textureLowTextHover.gameObject.SetActive(false);
            textureMedTextHover.gameObject.SetActive(true);
            textureHighTextHover.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Textures") == 2)
        {
            QualitySettings.masterTextureLimit = 0;
            textureLowText.GetComponent<Text>().text = "low";
            textureMedText.GetComponent<Text>().text = "med";
            textureHighText.GetComponent<Text>().text = "HIGH";

            textureLowTextHover.gameObject.SetActive(false);
            textureMedTextHover.gameObject.SetActive(false);
            textureHighTextHover.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = musicSlider.GetComponent<Slider>().value;
        sliderValueSFX = sfxSlider.GetComponent<Slider>().value;
    }

    // Change to fullscreen or windowed
    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
        {
            fullScreenText.GetComponent<Text>().text = "off";
        }
        else
        {
            fullScreenText.GetComponent<Text>().text = "on";
        }
    }
    
    // Change music slider
    public void MusicSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    // Change sfx slider
    public void SFXSlider()
    {
        PlayerPrefs.SetFloat("SFXVolume", sliderValueSFX);
    }

    public void ShowHUD()
    {
        if (PlayerPrefs.GetInt("ShowHUD") == 0)
        {
            PlayerPrefs.GetInt("ShowHUD", 1);
            showHudText.GetComponent<Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("ShowHUD") == 0)
        {
            PlayerPrefs.GetInt("ShowHUD", 0);
            showHudText.GetComponent<Text>().text = "off";
        }
    }

    public void ShadowsOff()
    {
        PlayerPrefs.SetInt("Shadows", 0);
        QualitySettings.shadowCascades = 0;
        QualitySettings.shadowDistance = 0;
        shadowOffText.GetComponent<Text>().text = "OFF";
        shadowLowText.GetComponent<Text>().text = "low";
        shadowHighText.GetComponent<Text>().text = "high";

        shadowOffTextHover.gameObject.SetActive(true);
        shadowLowTextHover.gameObject.SetActive(false);
        shadowHighTextHover.gameObject.SetActive(false);
    }

    public void ShadowsLow()
    {
        PlayerPrefs.SetInt("Shadows", 1);
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowDistance = 75;
        shadowOffText.GetComponent<Text>().text = "off";
        shadowLowText.GetComponent<Text>().text = "LOW";
        shadowHighText.GetComponent<Text>().text = "high";

        shadowOffTextHover.gameObject.SetActive(false);
        shadowLowTextHover.gameObject.SetActive(true);
        shadowHighTextHover.gameObject.SetActive(false);
    }

    public void ShadowsHigh()
    {
        PlayerPrefs.SetInt("Shadows", 2);
        QualitySettings.shadowCascades = 4;
        QualitySettings.shadowDistance = 400;
        shadowOffText.GetComponent<Text>().text = "off";
        shadowLowText.GetComponent<Text>().text = "low";
        shadowHighText.GetComponent<Text>().text = "HIGH";

        shadowOffTextHover.gameObject.SetActive(false);
        shadowLowTextHover.gameObject.SetActive(false);
        shadowHighTextHover.gameObject.SetActive(true);
    }

    public void vsync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            vsyncText.GetComponent<Text>().text = "on";
        } 
        else if (QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
            vsyncText.GetComponent<Text>().text = "off";
        }
    }

    public void InvertMouse()
    {
        if (PlayerPrefs.GetInt("Inverted") == 0)
        {
            PlayerPrefs.SetInt("Inverted", 1);
            invertMouseText.GetComponent<Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            PlayerPrefs.SetInt("Inverted", 0);
            invertMouseText.GetComponent<Text>().text = "off";
        }
    }

    public void AmbientOcclusion()
    {
        if (PlayerPrefs.GetInt("AmbientOcclusion") == 0)
        {
            PlayerPrefs.SetInt("AmbientOcclusion", 1);
            ambientOcclusionText.GetComponent<Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("AmbientOcclusion") == 1)
        {
            PlayerPrefs.SetInt("AmbientOcclusion", 0);
            ambientOcclusionText.GetComponent<Text>().text = "off";
        }
    }

    public void CameraEffects()
    {
        if (PlayerPrefs.GetInt("CameraEffects") == 0)
        {
            PlayerPrefs.SetInt("CameraEffects", 1);
            cameraEffectsText.GetComponent<Text>().text = "on";
        }
        else if (PlayerPrefs.GetInt("CameraEffects") == 1)
        {
            PlayerPrefs.SetInt("CameraEffects", 0);
            cameraEffectsText.GetComponent<Text>().text = "off";
        }
    }

    public void TexturesLow()
    {
        PlayerPrefs.SetInt("Textures", 0);
        QualitySettings.masterTextureLimit = 2;
        textureLowText.GetComponent<Text>().text = "LOW";
        textureMedText.GetComponent<Text>().text = "med";
        textureHighText.GetComponent<Text>().text = "high";

        textureLowTextHover.gameObject.SetActive(true);
        textureMedTextHover.gameObject.SetActive(false);
        textureHighTextHover.gameObject.SetActive(false);
    }

    public void TexturesMed()
    {
        PlayerPrefs.SetInt("Textures", 1);
        QualitySettings.masterTextureLimit = 1;
        textureLowText.GetComponent<Text>().text = "low";
        textureMedText.GetComponent<Text>().text = "MED";
        textureHighText.GetComponent<Text>().text = "high";

        textureLowTextHover.gameObject.SetActive(false);
        textureMedTextHover.gameObject.SetActive(true);
        textureHighTextHover.gameObject.SetActive(false);
    }

    public void TexturesHigh()
    {
        PlayerPrefs.SetInt("Textures", 2);
        QualitySettings.masterTextureLimit = 0;
        textureLowText.GetComponent<Text>().text = "low";
        textureMedText.GetComponent<Text>().text = "med";
        textureHighText.GetComponent<Text>().text = "HIGH";

        textureLowTextHover.gameObject.SetActive(false);
        textureMedTextHover.gameObject.SetActive(false);
        textureHighTextHover.gameObject.SetActive(true);
    }
}
