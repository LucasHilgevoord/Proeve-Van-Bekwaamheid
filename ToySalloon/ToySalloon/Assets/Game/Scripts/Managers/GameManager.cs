using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private bool isCreated;

    [Header("Store data")]
    public int storeMoney = 0; // The ammount of money the player has.
    public int storeRating = 0; // Current rated stars of the store.
    public int playerLevel = 0; // The level of the player.

    [Header("Player data")]
    public string playerName = "";
    public string hairSkin = "skin01";
    public string bodySkin = "f_skin03";
    public Gender gender = Gender.FEMALE;

    internal override void Awake()
    {
        base.Awake();
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
        }
    }

    private void SaveData()
    {

    }

    private void LoadData()
    {

    }
}
