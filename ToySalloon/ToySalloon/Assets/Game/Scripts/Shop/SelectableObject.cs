using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectableObject : MonoBehaviour
{
    private Vector3 myScale; // Start scale of the selected object.
    private float scaleFactor = 1.1f; // How much the object will scale ontop of the start scale.
    private float scaleSpeed = 0.1f; // How fast the object will scale when clicked.
    private GameObject selectedObject;

    private void OnEnable()
    {
        InputManager.OnObjectClicked += OnSelected;
    }
    private void OnDisable()
    {
        InputManager.OnObjectClicked -= OnSelected;
    }

    private void OnSelected(Transform t)
    {
        if (t.gameObject.tag == "Selectable" && t.gameObject != selectedObject)
        {
            selectedObject = t.gameObject;
            myScale = selectedObject.transform.localScale;
            Resize();
        }
    }

    private void Resize()
    {
        selectedObject.transform.DOScale(myScale * scaleFactor, scaleSpeed).SetEase(Ease.Linear).OnComplete(() => selectedObject.transform.DOScale(myScale, scaleSpeed).SetEase(Ease.Linear));
    }

}
