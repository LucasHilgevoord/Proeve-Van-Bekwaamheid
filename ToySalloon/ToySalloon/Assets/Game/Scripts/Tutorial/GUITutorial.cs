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
    private List<GameObject> normalObjects = new List<GameObject>();

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

    private WriteText write;

    private string currentText;
    private bool active = true;

    private bool nextActive = true;

    private void OnEnable()
    {
        WriteText.OnFinished += ShowNext;
    }

    private void OnDisable()
    {
        WriteText.OnFinished -= ShowNext;
    }

    private void Awake()
    {
        write = GetComponent<WriteText>();
    }

    public void LinkUI(string title, string textbox, HandleTutorial.tutState state, int index)
    {
        switch (state)
        {
            case HandleTutorial.tutState.BIG:
                if (this.bigTitle.text == "")
                {
                    this.bigTitle.text = title;
                }

                ShowNext();
                StartCoroutine(write.ShowMessage(textbox, bigTextbox));

                break;
            case HandleTutorial.tutState.SMALL:
                if (this.smallTitle.text == "")
                {
                    this.smallTitle.text = title;
                }

                StartCoroutine(write.ShowMessage(textbox, smallTextbox));
                sceneTutorial.SetInteger("NextPart", index);

                break;
        }
    }

    public void ShowNext()
    {
        nextActive = !nextActive;
        next.gameObject.SetActive(nextActive);
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
        foreach (GameObject obj in normalObjects)
        {
            obj.SetActive(true);
        }
    }

    public void CloseTutorialGeneral()
    {
        if (bigTutorialCanvas.gameObject.activeSelf)
        {
            bigTutorialCanvas.GetComponentInParent<Animator>().SetTrigger("CloseBigTutorial");
        }
        else if (smallTextbox.gameObject.activeSelf)
        {
            smallTutorialCanvas.SetTrigger("CloseSmallTut");
        }

        foreach (GameObject obj in normalObjects)
        {
            obj.SetActive(true);
        }
    }
}
