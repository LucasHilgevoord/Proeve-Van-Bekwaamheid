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
        winTitel.text = puzzle.puzzle.puzzleName;
        coinRewardWin.text = puzzle.puzzle.coinReward.ToString();

        winScreen.SetActive(true);
    }
    
    public void Lose()
    {
        loseTitel.text = puzzle.puzzle.puzzleName;
        loseTip.text = puzzle.puzzle.tip;
        coinRewardLose.text = puzzle.puzzle.coinReward.ToString();

        loseScreen.SetActive(true);
    }
}
