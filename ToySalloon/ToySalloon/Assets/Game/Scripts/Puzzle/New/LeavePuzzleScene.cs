﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePuzzleScene : MonoBehaviour
{
    public void CallSceneSwitch()
    {
        gameObject.SetActive(true);
    }

    public void Switch()
    {
        SceneManager.Instance.FadeToScene(2);
    }
}
