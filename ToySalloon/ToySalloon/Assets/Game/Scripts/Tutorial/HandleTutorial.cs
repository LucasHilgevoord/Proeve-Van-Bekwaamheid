using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTutorial : MonoBehaviour
{
    [SerializeField]
    private Tutorial sceneTutorial;

    [SerializeField]
    private List<GameObject> normalObjects = new List<GameObject>();
    [SerializeField]
    private Canvas tutorialCanvas;

    private GUITutorial tutorialInterface;

    private int textIndex = 0;

    public enum tutState { BIG, SMALL};
    private tutState currentState;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FirstPlay", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstPlay", 0);
            PlayerPrefs.Save();

            tutorialInterface = GetComponent<GUITutorial>();
            currentState = tutState.BIG;

            foreach (GameObject obj in normalObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in normalObjects)
            {
                obj.SetActive(true);
            }
            tutorialCanvas.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ManageBigTutorial();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<WriteText>().type == WriteText.typingState.NORMAL)
        {
            switch (currentState)
            {
                case tutState.BIG:
                    ManageBigTutorial();
                    break;
                case tutState.SMALL:
                    ManageSmallTutorial();
                    break;
            }

        }
        else if (Input.GetMouseButtonDown(0) && GetComponent<WriteText>().type == WriteText.typingState.TYPING)
        {
            GetComponent<WriteText>().type = WriteText.typingState.NORMAL;
        }

    }
    private void ManageBigTutorial()
    {
        if (textIndex < sceneTutorial.bigMessage.Length)
        {
            LinkInterface(textIndex);
        }
        else
        {
            if(sceneTutorial.smallMessage.Length != 0)
            {
                tutorialInterface.CloseBigTutorial();
                currentState = tutState.SMALL;
                textIndex = 0;
                LinkInterface(textIndex);
            }
            else
            {
                tutorialInterface.CloseTutorialGeneral();
            }
        }
    }

    private void ManageSmallTutorial()
    {
        if (textIndex < sceneTutorial.smallMessage.Length)
        {
            LinkInterface(textIndex);
        }
        else
        {
            tutorialInterface.CloseSmallTutorial();
        }
    }

    private void LinkInterface(int index)
    {
        switch (currentState)
        {
            case tutState.BIG:
                tutorialInterface.LinkUI(sceneTutorial.title, sceneTutorial.bigMessage[index], currentState, textIndex);
                break;
            case tutState.SMALL:
                tutorialInterface.LinkUI(sceneTutorial.title, sceneTutorial.smallMessage[index], currentState, textIndex);
                break;
        }
        textIndex++;
    }
}
