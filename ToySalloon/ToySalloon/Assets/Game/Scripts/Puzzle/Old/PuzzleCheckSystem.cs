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
        // Assign the correct order
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
        // Check if all pieces are placed down
        if (manager.placedPieces.Count >= 4)
        {
            confirmButton.enabled = true;
        }
        else
        {
            confirmButton.enabled = false;
        }

        // Change button alpha
        if (confirmButton.enabled)
        {
            confirmButton.image.color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            confirmButton.image.color = new Color(255f, 255f, 255f, .8f);
        }
    }

    // Function to check whether all conditions are met
    public void OnConfirm()
    {
        // Iterate through all pieces
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

        // Check how many pieces are correct
        if (correctPieces >= 4)
        {
            Application.LoadLevel(1);
        }
        else
        {
            return;
        }
    }

    // Clear the arrays
    public void OnClear()
    {
        foreach (GameObject piece in manager.placedPieces)
        {
            piece.transform.position = new Vector3(piece.transform.position.x * 2, piece.transform.position.y, piece.transform.position.z);
        }
        manager.placedPieces.Clear();
        manager.puzzleOrder.Clear();
    }
}
