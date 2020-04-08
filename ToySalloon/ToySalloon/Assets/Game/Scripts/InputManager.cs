using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void ObjectClicked(Transform obj);
    public static event ObjectClicked onObjectClicked;

    public delegate void TouchHold();
    public static event TouchHold onTouchHold;

    public delegate void MultiTouch();
    public static event MultiTouch onMultiTouch;

    private float touchDelay = 0.5f;

    void Update()
    {
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

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform hitObj = hit.transform;
                onObjectClicked(hitObj);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            onTouchHold();
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            onMultiTouch();
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
                    onObjectClicked(hitObj);
                }
            }

            if (t.phase == TouchPhase.Moved)
            {
                //Hold action
                onTouchHold();
            }
        }
        else if(Input.touchCount > 1)
        {
            onMultiTouch();
        }
    }
}
