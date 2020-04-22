using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public delegate void WindowOverlay(bool isOpen);
    public static event WindowOverlay OnWindowOverlay;

    private void OnEnable()
    {
        EventDelay();
    }

    private void EventDelay()
    {
        //Small delay before sending out the event.
        float time = 1f;
        DOTween.To(() => time, f => time = f, 0, 0.1f).OnComplete(() =>
        {
            OnWindowOverlay(true);
        });
    }

    private void OnDisable()
    {
        OnWindowOverlay(false);
    }
}
