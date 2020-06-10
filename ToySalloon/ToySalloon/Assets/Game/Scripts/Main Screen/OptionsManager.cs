using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private Animator cameraObject;

    // Panels
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private GameObject videoPanel;

    // Sounds
    [SerializeField]
    private GameObject hoverSound;
    [SerializeField]
    private GameObject sfxHoverSound;
    [SerializeField]
    private GameObject clickSound;

    // Highlights
    [SerializeField]
    private GameObject lineGame;
    [SerializeField]
    private GameObject lineVideo;

    void Start()
    {
        GamePanel();
    }

    // Game settings panel
    public void GamePanel()
    {
        videoPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);

        lineGame.gameObject.SetActive(true);
        lineVideo.gameObject.SetActive(false);
    }

    // Video settings panel
    public void VideoPanel()
    {
        videoPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);

        lineGame.gameObject.SetActive(false);
        lineVideo.gameObject.SetActive(true);
    }


    // Hover sounds

    // Play hover sound 
    public void PlayHover()
    {
        hoverSound.GetComponent<AudioSource>().Play();
    }

    // Play sfx hover sound 
    public void PlaySFXHover()
    {
        sfxHoverSound.GetComponent<AudioSource>().Play();
    }

    // Play click hover sound 
    void PlayClick()
    {
        clickSound.GetComponent<AudioSource>().Play();
    }
}
