using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine; 

public class GUITutorial : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField]
    private GameObject bigTutorialCanvas;
    [SerializeField]
    private Animator smallTutorialCanvas;
    [SerializeField]
    private Animator sceneTutorial;

    [SerializeField]
    private Canvas normalCanvas;

    [Header("Text fields")]
    [SerializeField]
    private TextMeshProUGUI bigTitle;
    [SerializeField]
    private TextMeshProUGUI bigTextbox;
    [SerializeField]
    private TextMeshProUGUI smallTitle;
    [SerializeField]
    private TextMeshProUGUI smallTextbox;

    [Header("Next text")]
    [SerializeField]
    private Animator next;

    [Header("Typing speed")]
    [SerializeField, Range(0.02f, 0.2f)]
    private float typeSpeed;

    private string currentText;
    private bool active = true;

    public void LinkUI(string title, string textbox, HandleTutorial.tutState state, int index)
    {
        switch (state)
        {
            case HandleTutorial.tutState.BIG:
                if (this.bigTitle.text == "")
                {
                    this.bigTitle.text = title;
                }

                StartCoroutine(ShowMessage(textbox, bigTextbox));

                break;
            case HandleTutorial.tutState.SMALL:
                if (this.smallTitle.text == "")
                {
                    this.smallTitle.text = title;
                }

                StartCoroutine(ShowMessage(textbox, smallTextbox));
                sceneTutorial.SetInteger("NextPart", index);

                break;
        }
    }

    private IEnumerator ShowMessage(string mes, TextMeshProUGUI box)
    {
        box.text = "";
        ShowNext(false);
        foreach (char letter in mes.ToCharArray())
        {
            Debug.Log(letter);
            box.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        Debug.Log("works");
        ShowNext(true);
    }

    public void ShowNext(bool a)
    {
        next.gameObject.SetActive(a);
    }

    public void HandleSceneTutorial()
    {
        smallTutorialCanvas.gameObject.SetActive(true);
        sceneTutorial.gameObject.SetActive(true);
    }

    public void CloseBigTutorial()
    {
        bigTutorialCanvas.GetComponentInParent<Animator>().SetTrigger("CloseBigTutorial");
        HandleSceneTutorial();
    }

    public void CloseSmallTutorial()
    {
        smallTutorialCanvas.SetTrigger("CloseSmallTut");
        normalCanvas.gameObject.SetActive(true);
    }
}
