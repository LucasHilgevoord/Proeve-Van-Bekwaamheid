using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeHandler : MonoBehaviour
{
    [SerializeField]
    private Image overlay;
    private float fadeSpeed = 1;

    [SerializeField]
    private bool fadeWhenStart = true;

    private void OnEnable()
    {
        WorldManager.OnFade += FadeToScene;
        FadeOut();
    }

    private void OnDisable()
    {
        WorldManager.OnFade += FadeToScene;
    }

    /// <summary>
    /// Fade the screen and go to assigned scene if needed.
    /// </summary>
    /// <param name="sceneID"></param>
    public void FadeToScene(int sceneID)
    {
        float alpha = 0f;
        DOTween.To(() => alpha, f => alpha = f, 1f, fadeSpeed).OnUpdate(() =>
        {
            overlay.color = new Color(0, 0, 0, alpha);
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            Application.LoadLevel(sceneID);
        });
    }

    public void FadeOut()
    {
        if (!fadeWhenStart) return;

        float alpha = 1f;
        DOTween.To(() => alpha, f => alpha = f, 0f, fadeSpeed).OnUpdate(() =>
        {
            overlay.color = new Color(0, 0, 0, alpha);
        }).SetEase(Ease.Linear);
    }
}
