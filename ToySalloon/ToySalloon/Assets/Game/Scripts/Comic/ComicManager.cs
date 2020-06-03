using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComicManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> comicImages = new List<GameObject>();

    [SerializeField]
    private AudioSource[] audioSources;

    private void Start()
    {
        StartCoroutine(HandleComics());
    }

    private IEnumerator HandleComics()
    {
        for (int i = 0; i < comicImages.Count; i++)
        {
            comicImages[i].SetActive(true);

            switch (i)
            {
                case 1:
                    audioSources[1].Play();
                    yield return new WaitForSeconds(2f);
                    break;
                case 4:
                    audioSources[0].Play();
                    yield return new WaitForSeconds(6f);
                    break;
                case 6:
                    audioSources[2].Play();
                    yield return new WaitForSeconds(2f);
                    break;
                default:
                    yield return new WaitForSeconds(2f);
                    break;
            }
        }
    }

    public void Continue()
    {
        SceneManager.Instance.FadeToScene(1);
    }
}
