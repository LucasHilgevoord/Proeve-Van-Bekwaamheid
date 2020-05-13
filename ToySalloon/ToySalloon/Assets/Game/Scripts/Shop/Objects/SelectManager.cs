using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Checks if the object which the user tapped on is a selectable.
/// </summary>
public class SelectManager : MonoBehaviour
{
    private GameObject selectedObject;
    [SerializeField] SelectWindow window;

    private void OnEnable()
    {
        InputManager.OnObjectClicked += OnSelected;
        SelectWindow.OnDisabled += DeselectObject;
    }
    private void OnDisable()
    {
        InputManager.OnObjectClicked -= OnSelected;
        SelectWindow.OnDisabled -= DeselectObject;
    }

    private void OnSelected(Transform t)
    {
        SelectableObject scr = t.GetComponent<SelectableObject>();
        if (scr)
        {
            // Check if the newest selected is the same as the old selected
            if (t.gameObject != selectedObject)
            {
                // Select object
                selectedObject = t.gameObject;
                scr.OnSelected();

                window.gameObject.SetActive(true);
                window.selectedObject = selectedObject;
            }
        }
    }

    private void DeselectObject()
    {
        selectedObject = null;
    }
}
