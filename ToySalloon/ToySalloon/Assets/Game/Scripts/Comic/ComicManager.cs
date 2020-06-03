using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComicManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> comicImages = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(HandleComics());
    }

    private IEnumerator HandleComics()
    {
        for (int i = 0; i < comicImages.Count; i++)
        {
            comicImages[i].SetActive(true);
            if(i == 4)
            {
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void Continue()
    {
        SceneManager.Instance.FadeToScene(1);
    }
}
