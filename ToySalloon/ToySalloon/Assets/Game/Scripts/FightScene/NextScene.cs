using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void SceneSwitch()
    {
        SceneManager.Instance.FadeToScene(2);
    }
}
