using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] Animator closetAnim;

    [SerializeField] GameObject nameWindow;
    [SerializeField] TMP_InputField field;

    public void OnCompleteButton()
    {
        if (GameManager.Instance.playerName == "")
        {
            nameWindow.SetActive(true);
        } else
        {
            OnFinish();
        }
    }

    public void SetPlayerName()
    {
        if (field.text == "") return;
        GameManager.Instance.playerName = field.text;
        nameWindow.GetComponent<Animator>().SetBool("Close", true);
        OnFinish();
    }

    public void CancelPlayerName()
    {
        nameWindow.SetActive(false);
    }

    private void OnFinish()
    {
        closetAnim.SetBool("ClosetClose", true);
        SceneManager.Instance.FadeToScene(1, 1.5f);
    }
}
