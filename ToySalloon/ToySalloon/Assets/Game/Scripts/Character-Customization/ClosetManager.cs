using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetManager : MonoBehaviour
{
    public SpriteRenderer characterPart;

    [SerializeField]
    private List<GameObject> drawers = new List<GameObject>();

    private GameObject currentDrawer;

    private void Start()
    {
        currentDrawer = drawers[0];

        CheckActive();
    }

    public void SwitchDrawer(GameObject drawer)
    {
        currentDrawer = drawer;
        CheckActive();
    }

    private void CheckActive()
    {
        foreach (GameObject drawer in drawers)
        {
            if (drawer == currentDrawer)
            {
                drawer.gameObject.SetActive(true);
                characterPart = drawer.GetComponent<ClosetPlayerLink>().part;
            }
            else
            {
                drawer.gameObject.SetActive(false);
            }
        }
    }
}
