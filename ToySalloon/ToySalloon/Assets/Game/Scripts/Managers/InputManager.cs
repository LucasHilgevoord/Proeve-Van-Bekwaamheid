﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void ObjectClicked(Transform obj);
    public static event ObjectClicked OnObjectClicked;

    public delegate void TouchHold();
    public static event TouchHold OnTouchHold;

    public delegate void MultiTouch();
    public static event MultiTouch OnMultiTouch;

    private float touchDelay = 0.5f;
    private bool disableTouch = false;

    private void OnEnable()
    {
        UIWindow.OnWindowOverlay += AllowTouch;
    }

    private void AllowTouch(bool t)
    {
        disableTouch = t;
    }

    private void Update()
    {
        if (disableTouch) return;

        if (Application.isEditor)
        {
            MouseInput();
        } else
        {
            MobileInput();
        }
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform hitObj = hit.transform;
                OnObjectClicked(hitObj);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            OnTouchHold();
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            OnMultiTouch();
        }
    }

    private void MobileInput()
    {
        //Single touch
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Ended && t.deltaTime < touchDelay)
            {
                //Tap action
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Transform hitObj = hit.transform;
                    OnObjectClicked(hitObj);
                }
            }

            if (t.phase == TouchPhase.Moved)
            {
                //Hold action
                OnTouchHold();
            }
        }
        else if(Input.touchCount > 1)
        {
            //Multi hold action
            OnMultiTouch();
        }
    }
}