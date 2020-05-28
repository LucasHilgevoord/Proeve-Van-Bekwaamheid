using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public delegate void ObjectClicked(Transform obj);
    public static event ObjectClicked OnObjectClicked;

    public delegate void TouchHold();
    public static event TouchHold OnTouchHold;

    public delegate void MultiTouch();
    public static event MultiTouch OnMultiTouch;

    private float touchReset = 0.5f;
    private float touchResetDelay = 0f;
    private float touchDelay = 0.5f;
    private bool disableTouch = false;

    private void OnEnable()
    {
        UIWindow.OnWindowOverlay += AllowTouch;
    }
    private void OnDisable()
    {
        UIWindow.OnWindowOverlay -= AllowTouch;
    }

    private void AllowTouch(bool t)
    {
        disableTouch = t;
    }

    private void Update()
    {
        if (disableTouch) return;

        if (!Application.isMobilePlatform)
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
            if (EventSystem.current.IsPointerOverGameObject()) return;
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
        if (touchResetDelay > 0)
        {
            touchResetDelay -= Time.deltaTime;
        }

        //Single touch
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                //Hold action
                OnTouchHold();
            } else if (t.phase == TouchPhase.Ended && t.deltaTime < touchDelay && touchResetDelay <= 0)
            {
                touchResetDelay = touchReset;
                //Tap action
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Transform hitObj = hit.transform;
                    OnObjectClicked(hitObj);
                }
            }
        }
        else if (Input.touchCount > 1)
        {
            //Multi hold action
            OnMultiTouch();
        }
    }
}
