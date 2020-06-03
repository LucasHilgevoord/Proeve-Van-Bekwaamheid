using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : SingletonBehaviour<SceneManager>
{
    [SerializeField]
    private Image overlay;
    private float fadeSpeed = 1;

    [SerializeField]
    private bool fadeWhenStart = true;

    private bool isFading;

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLevelLoaded;
    }

    /// <summary>
    /// Fade the screen and go to assigned scene if needed.
    /// </summary>
    /// <param name="sceneID"></param>
    public void FadeToScene(int sceneID, float fadeSpeed = 1)
    {
        if (isFading) return;
        isFading = true;
        float alpha = 0f;
        DOTween.To(() => alpha, f => alpha = f, 1f, fadeSpeed).OnUpdate(() =>
        {
            overlay.color = new Color(0, 0, 0, alpha);
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
            isFading = false;
        });
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
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
