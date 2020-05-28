using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCondition : MonoBehaviour
{
    public GameObject loseScreen;

    [SerializeField]
    private GameObject winScreen;

    [Header("WinUI")]
    [SerializeField]
    private Text winTitel;
    [SerializeField]
    private Text coinRewardWin;

    [Header("LoseUI")]
    [SerializeField]
    private Text loseTitel;
    [SerializeField]
    private Text loseTip;
    [SerializeField]
    private Text coinRewardLose;

    private HandlePuzzle puzzle;

    private void Start()
    {
        puzzle = GetComponent<HandlePuzzle>();
    }

    public void Win()
    {
        winTitel.text = puzzle.currentPuzzle.puzzleName;
        coinRewardWin.text = puzzle.currentPuzzle.coinReward.ToString();

        winScreen.SetActive(true);
    }
    
    public void Lose()
    {
        loseTitel.text = puzzle.currentPuzzle.puzzleName;
        loseTip.text = puzzle.currentPuzzle.tip;
        coinRewardLose.text = puzzle.currentPuzzle.coinReward.ToString();

        loseScreen.SetActive(true);
    }
}
