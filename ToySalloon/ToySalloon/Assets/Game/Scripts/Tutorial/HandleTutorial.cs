using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTutorial : MonoBehaviour
{
    [SerializeField]
    private Tutorial sceneTutorial;

    private GUITutorial tutorialInterface;

    private int textIndex = 0;

    private void Start()
    {
        tutorialInterface = GetComponent<GUITutorial>();
        LinkInterface(textIndex);
    }

    private void LinkInterface(int index)
    {
        tutorialInterface.LinkUI(sceneTutorial.title, sceneTutorial.bigMessage[index]);
        textIndex++;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManageTutorial();
        }

    }

    private void ManageTutorial()
    {
        if (textIndex < sceneTutorial.bigMessage.Length)
        {
            LinkInterface(textIndex);
        }
        else
        {
            Debug.Log("nope :)");
        }
    }
}
