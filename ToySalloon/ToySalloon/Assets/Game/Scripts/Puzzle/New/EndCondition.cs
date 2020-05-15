using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;

    public GameObject loseScreen;

    public void Win()
    {
        winScreen.SetActive(true);
    }
    
    public void Lose()
    {
        loseScreen.SetActive(true);
    }
}
