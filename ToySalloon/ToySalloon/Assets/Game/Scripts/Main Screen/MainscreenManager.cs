using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainscreenManager : MonoBehaviour
{
    [SerializeField]
    private Canvas selectCanvas;
    [SerializeField]
    private Animator logoCanvas;
    [SerializeField]
    private Animator logo;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        selectCanvas.gameObject.SetActive(true);
        Camera.main.GetComponent<Animator>().SetTrigger("GotoSelect");
        logo.SetTrigger("LogoOutro");
        logoCanvas.SetTrigger("KlikOpOutro");
    }

    public void LoadGame()
    {
        if(PlayerPrefs.GetString("PlayerName") != "")
        {
            SceneManager.Instance.FadeToScene(2);
            GameManager.Instance.LoadData();
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        SceneManager.Instance.FadeToScene(1);
        PlayerPrefs.SetInt("FirstPlay", 1);
    }
}
