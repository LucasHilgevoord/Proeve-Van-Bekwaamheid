using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectableObject : MonoBehaviour
{
    private Vector3 myScale; // Start scale of the selected object.
    private float scaleFactor = 1.1f; // How much the object will scale ontop of the start scale.
    private float scaleSpeed = 0.1f; // How fast the object will scale when clicked.

    public SelectableSkins skins;
    public GameObject modelParent;

    public void OnSelected()
    {
        myScale = transform.localScale;
        Resize();
    }

    private void Resize()
    {
        transform.DOScale(myScale * scaleFactor, scaleSpeed).SetEase(Ease.Linear).OnComplete(() => transform.DOScale(myScale, scaleSpeed).SetEase(Ease.Linear));
    }

}
