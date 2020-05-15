using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePuzzle : MonoBehaviour
{
    [SerializeField]
    private InfoPuzzle puzzle;

    [SerializeField]
    private PuzzlePiece puzzlePiece;

    [SerializeField]
    private ContentSizeFitter content;

    private List<PuzzlePiece> spawnedPieces = new List<PuzzlePiece>();

    private int pieces;

    private void Start()
    {
        InstantiatePieces();
    }

    //Instantiates all the pieces of the correct puzzle.
    private void InstantiatePieces()
    {
        pieces = puzzle.lines.Length;

        for (int i = 0; i < pieces; i++)
        {
            spawnedPieces.Add(puzzlePiece);

            PuzzlePiece piece = Instantiate(spawnedPieces[i], content.transform) as PuzzlePiece;
            piece.idOrder = i;
            piece.ChangeLine(puzzle.lines[i] + " (" + (piece.idOrder + 1) + ")");

            piece.transform.SetSiblingIndex(Random.Range(0, spawnedPieces.Count));
        }
    }
}
