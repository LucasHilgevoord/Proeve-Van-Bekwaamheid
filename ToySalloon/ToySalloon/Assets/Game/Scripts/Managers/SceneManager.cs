using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : SingletonBehaviour<SceneManager>
{
    [SerializeField]
    private Image overlay;
    private float fadeSpeed = 1;

    [SerializeField]
    private bool fadeWhenStart = true;

    /// <summary>
    /// Fade the screen and go to assigned scene if needed.
    /// </summary>
    /// <param name="sceneID"></param>
    public void FadeToScene(int sceneID, float fadeSpeed = 1)
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

    private void OnLevelWasLoaded(int level)
    {
        FadeOut();
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
