using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObjects : MonoBehaviour
{
    private float resizeSpeed = 0.3f;
    private float delay = 0;
    private float delayMultiplier = 0.05f;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.localScale = new Vector2(0, 0);

            delay += delayMultiplier;
            StartCoroutine(Resize(child));
        }
    }

    private void OnDisable()
    {
        delay = 0;
    }

    IEnumerator Resize(Transform obj)
    {
        yield return new WaitForSeconds(delay);
        float openSize = 0;

        DOTween.To(() => openSize, f => openSize = f, 1f, resizeSpeed).OnUpdate(() =>
        {
            obj.localScale = new Vector2(openSize, openSize);
        });
    }
}
