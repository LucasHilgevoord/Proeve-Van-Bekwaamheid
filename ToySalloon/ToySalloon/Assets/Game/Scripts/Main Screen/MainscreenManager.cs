﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainscreenManager : MonoBehaviour
{
    [SerializeField]
    private Canvas selectCanvas;
    [SerializeField]
    private Canvas optionCanvas;
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

        if (Input.GetKeyDown(KeyCode.O))
        {
            OptionsMenu();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BacktoSelect();
        }
    }

    private void ChangeState()
    {
        selectCanvas.gameObject.SetActive(true);
        Camera.main.GetComponent<Animator>().SetTrigger("GotoSelect");
        logo.SetTrigger("LogoOutro");
        logoCanvas.SetTrigger("KlikOpOutro");
    }

    private void OptionsMenu()
    {
        optionCanvas.gameObject.SetActive(true);
        Camera.main.GetComponent<Animator>().SetTrigger("GotoOptions");
        logo.SetTrigger("LogoOutro");
        logoCanvas.SetTrigger("KlikOpOutro");
    }

    private void BacktoSelect()
    {
        optionCanvas.gameObject.SetActive(false);
        Camera.main.GetComponent<Animator>().SetTrigger("BacktoSelect");
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
        for (int i = 1; i <= 2; i++)
        {
            PlayerPrefs.SetInt("FirstPlay" + i, 1);
            Debug.Log(PlayerPrefs.GetInt("FirstPlay" + i));
        }
        PlayerPrefs.Save();
    }
}
