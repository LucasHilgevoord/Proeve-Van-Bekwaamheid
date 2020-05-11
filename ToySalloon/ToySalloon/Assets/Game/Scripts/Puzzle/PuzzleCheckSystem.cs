using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCheckSystem : MonoBehaviour
{
    [SerializeField]
    private PlaceHolderManager manager;

    private List<int> correctOrder = new List<int>();
    private float correctPieces;

    public Button confirmButton;
    void Start()
    {
        correctOrder.Add(1);
        correctOrder.Add(2);
        correctOrder.Add(3);
        correctOrder.Add(4);

        manager = GameObject.FindObjectOfType(typeof(PlaceHolderManager)) as PlaceHolderManager;
        confirmButton.enabled = false;
        correctPieces = 0;
    }

    void Update()
    {
        if (manager.placedPieces.Count >= 4)
        {
            confirmButton.enabled = true;
        }

        if (confirmButton.enabled)
        {
            confirmButton.image.color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            confirmButton.image.color = new Color(255f, 255f, 255f, .8f);
        }
    }

    public void OnConfirm()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (manager.puzzleOrder[i] != correctOrder[i])
            {
                Debug.Log("Wrong");
            } else
            {
                correctPieces++;
            }
        }

        if (correctPieces >= 4)
        {
            Debug.Log("PASSED!");
            Application.LoadLevel(1);
        }
        else
        {
            Debug.Log("Try again");
            return;
        }
    }
}
