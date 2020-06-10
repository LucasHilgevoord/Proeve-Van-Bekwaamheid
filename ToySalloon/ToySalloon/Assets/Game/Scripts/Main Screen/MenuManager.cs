using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager  : MonoBehaviour
{
    public Animator cameraObject;

    // Panels
    public GameObject gamePanel;
    public GameObject videoPanel;

    // Sounds
    public GameObject hoverSound;
    public GameObject sfxHoverSound;
    public GameObject clickSound;

    // Highlights
    public GameObject lineGame;
    public GameObject lineVideo;

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
