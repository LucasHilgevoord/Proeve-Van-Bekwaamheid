using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePuzzle : MonoBehaviour
{
    public InfoPuzzle puzzle;

    [SerializeField]
    private PuzzlePiece puzzlePiece;

    [SerializeField]
    private ContentSizeFitter content;
    [SerializeField]
    private ContentSizeFitter fieldContent;

    private EndCondition endCondition;

    private int correctPieces;

    private List<PuzzlePiece> spawnedPieces = new List<PuzzlePiece>();

    private int pieces;

    private void Start()
    {
        StartCoroutine(InstantiatePieces(0.1f));
        endCondition = GetComponent<EndCondition>();
    }

    //Instantiates all the pieces of the correct puzzle.
    private IEnumerator InstantiatePieces(float time)
    {
        pieces = puzzle.lines.Length;

        for (int i = 0; i < pieces; i++)
        {
            yield return new WaitForSeconds(time);
            spawnedPieces.Add(puzzlePiece);
            PuzzlePiece piece = Instantiate(spawnedPieces[i], content.transform) as PuzzlePiece;
            piece.gameObject.SetActive(false);
            piece.idOrder = i;
            piece.ChangeLine(puzzle.lines[i] + " (" + (piece.idOrder + 1) + ")");

            piece.transform.SetSiblingIndex(Random.Range(0, spawnedPieces.Count));
        }

        for (int i = 0; i < pieces; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ResetPieces()
    {
        spawnedPieces.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
           Destroy(content.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < fieldContent.transform.childCount - 1; i++)
        {
            Destroy(fieldContent.transform.GetChild(i).gameObject);
        }

        if(fieldContent.transform.childCount > puzzle.lines.Length)
        {
            fieldContent.transform.GetChild(puzzle.lines.Length).gameObject.SetActive(true);
        }

        StartCoroutine(InstantiatePieces(0));
    }

    public void CheckOrder()
    {
        if(fieldContent.transform.childCount > puzzle.lines.Length)
        {
            for (int i = 0; i < fieldContent.transform.childCount - 1; i++)
            {
                if(fieldContent.transform.GetChild(i).GetComponent<PuzzlePiece>().idOrder == i)
                {
                    correctPieces++;
                }
                else
                {
                    correctPieces = 0;
                }
            }
        }
        else
        {
            Debug.Log("Didnt reach length!");
        }

        if(correctPieces == puzzle.lines.Length)
        {
            endCondition.Win();
            correctPieces = 0;
        }
        else
        {
            Debug.Log("Incorrect!");
            endCondition.Lose();
        }
    }

    public void RestartLevel()
    {
        endCondition.loseScreen.GetComponent<Animator>().SetTrigger("Close");

        ResetPieces();
    }
}
