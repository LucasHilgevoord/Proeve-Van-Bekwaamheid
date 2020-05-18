using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class GUITutorial : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private TextMeshProUGUI textbox;

    private string currentText;
    private bool active = true;

    public void LinkUI(string title, string textbox)
    {
        if(this.title.text == "")
        {
            this.title.text = title;
        }

        if (textbox != "")
        {
            StartCoroutine(ShowMessage(textbox));
        }
    }

    private IEnumerator ShowMessage(string mes)
    {
        for (int i = 0; i < mes.Length; i++)
        {
            currentText = mes.Substring(0, i);
            textbox.text = currentText;
            yield return new WaitForSeconds(0.03f);

            if (!active)
            {
                yield break;
            }
        }
    }
}
