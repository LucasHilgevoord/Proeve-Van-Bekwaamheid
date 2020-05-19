using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] Animator closetAnim;

    public void OnCompleteButton()
    {
        closetAnim.SetBool("ClosetClose", true);
        SceneManager.Instance.FadeToScene(1, 1.5f);
    }
}
