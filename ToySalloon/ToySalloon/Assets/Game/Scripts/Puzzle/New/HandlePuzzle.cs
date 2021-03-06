﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePuzzle : MonoBehaviour
{
    [Header("Current Puzzle")]
    public InfoPuzzle currentPuzzle;
    [SerializeField] InfoPuzzle[] puzzleList;

    [Header("Prefab")]
    [SerializeField]
    private PuzzlePiece puzzlePiece;

    [Header("PlayFields")]
    [SerializeField]
    private ContentSizeFitter content;
    [SerializeField]
    private ContentSizeFitter fieldContent;

    [SerializeField] private Transform modelSpot;
    private GameObject model;

    private EndCondition endCondition;

    private int correctPieces;

    private List<PuzzlePiece> spawnedPieces = new List<PuzzlePiece>();

    private int pieces;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstPlay", 2) == 1)
        {
            PlayerPrefs.SetInt("FirstPlay", 0);
            PlayerPrefs.Save();
            currentPuzzle = puzzleList[3];
        }
        else
        {
            currentPuzzle = puzzleList[Random.Range(0, puzzleList.Length)];
        }

        StartCoroutine(InstantiatePieces(0.1f));
        endCondition = GetComponent<EndCondition>();

        model = currentPuzzle.model;

        SpawnModel();
    } 

    private void SpawnModel()
    {
        GameObject newModel = Instantiate(model, modelSpot);
        newModel.transform.localPosition += currentPuzzle.offset;
        newModel.transform.localRotation = currentPuzzle.rot;
    }

    //Instantiates all the pieces of the correct puzzle.
    private IEnumerator InstantiatePieces(float time)
    {
        pieces = currentPuzzle.lines.Length;

        for (int i = 0; i < pieces; i++)
        {
            yield return new WaitForSeconds(time);
            spawnedPieces.Add(puzzlePiece);
            PuzzlePiece piece = Instantiate(spawnedPieces[i], content.transform) as PuzzlePiece;
            piece.gameObject.SetActive(false);
            piece.idOrder = i;
            piece.ChangeLine(currentPuzzle.lines[i] + " (" + (piece.idOrder + 1) + ")");

            piece.transform.SetSiblingIndex(Random.Range(0, spawnedPieces.Count));
        }

        for (int i = 0; i < pieces; i++)
        {
            if(i > 3)
            {
                ScrollDown(1);
            }

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

        if(fieldContent.transform.childCount > currentPuzzle.lines.Length)
        {
            fieldContent.transform.GetChild(currentPuzzle.lines.Length).gameObject.SetActive(true);
        }

        StartCoroutine(InstantiatePieces(0));
    }

    public void CheckOrder()
    {
        if(fieldContent.transform.childCount > currentPuzzle.lines.Length)
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
            return;
        }

        if(correctPieces == currentPuzzle.lines.Length)
        {
            endCondition.Win();
            correctPieces = 0;
        }
        else
        {
            endCondition.Lose();
        }
    }

    public void RestartLevel()
    {
        endCondition.loseScreen.GetComponent<Animator>().SetTrigger("Close");

        ResetPieces();
    }

    public void ScrollDown(int c)
    {
        if(c == 1)
        {
            if(content.transform.childCount > 4 && content.transform.childCount != currentPuzzle.lines.Length + 1)
            {
                content.transform.parent.GetComponentInParent<ScrollRect>().velocity = new Vector2(0f, 1000f);
            }
        }
        else
        {
            if (fieldContent.transform.childCount > 4 && fieldContent.transform.childCount != currentPuzzle.lines.Length + 1)
            {
                fieldContent.transform.parent.GetComponentInParent<ScrollRect>().velocity = new Vector2(0f, 1000f);
            }
        }
    }

}
