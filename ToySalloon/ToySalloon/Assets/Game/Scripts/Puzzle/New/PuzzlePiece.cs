using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public int idOrder;

    [SerializeField, TextArea(1,2)]
    private string pieceLine;

    private Text pieceTextBox;

    public void ChangeLine(string line)
    {
        pieceTextBox = GetComponentInChildren<Text>();
        pieceLine = line;
        pieceTextBox.text = pieceLine;
    }
}
