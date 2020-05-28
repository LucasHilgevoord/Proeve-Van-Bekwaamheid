using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerManager : MonoBehaviour
{
    [SerializeField] private GameObject currentDrawer;

    public void SwitchDrawer(GameObject drawer)
    {
        if (currentDrawer != null)
            currentDrawer.SetActive(false);

        currentDrawer = drawer;
        currentDrawer.SetActive(true);
    }
}
