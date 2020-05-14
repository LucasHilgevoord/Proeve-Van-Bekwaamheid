using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWindow : UIWindow
{
    public delegate void DisabledWindow();
    public static event DisabledWindow OnDisabled;

    public GameObject selectedObject;
    private GameObject currentWindow;

    public void DisableWindow()
    {
        if (currentWindow)
        {
            currentWindow.SetActive(false);
            currentWindow = null;
        }
        OnDisabled();
        gameObject.SetActive(false);

    }

    public void SetCurrentWindow(GameObject window)
    {
        if (window != currentWindow)
        {
            // Enable new window, Disable old window
            if (currentWindow)
                currentWindow.SetActive(false);

            window.SetActive(true);
            currentWindow = window;
        } else
        {
            // Disable current window
            DisableCurrentWindow();
        }
    }

    public void DisableCurrentWindow()
    {
        if (!currentWindow) return;
        currentWindow.SetActive(false);
        currentWindow = null;
    }
}
