using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTutorial : MonoBehaviour
{
    [SerializeField]
    private Tutorial sceneTutorial;

    [SerializeField]
    private Canvas normalCanvas;
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
        }
        else
        {
            normalCanvas.gameObject.SetActive(true);
            tutorialCanvas.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ManageBigTutorial();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(currentState)
            {
                case tutState.BIG:
                    ManageBigTutorial();
                    break;
                case tutState.SMALL:
                    ManageSmallTutorial();
                    break;
            }

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
            tutorialInterface.CloseBigTutorial();
            currentState = tutState.SMALL;
            textIndex = 0;
            LinkInterface(textIndex);
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
