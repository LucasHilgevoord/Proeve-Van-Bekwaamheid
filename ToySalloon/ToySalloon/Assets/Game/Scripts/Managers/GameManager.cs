using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private bool isCreated;

    [Header("Store data")]
    public int storeMoney = 0; // The ammount of money the player has.
    public int storeRating = 0; // Current rated stars of the store.
    public int storeLevel = 0; // The level of the player.

    [Header("Player data")]
    public string playerName = "";
    public string hairSkin = "skin01";
    public string bodySkin = "f_skin03";
    public Gender gender = Gender.FEMALE;

    private void OnDisable()
    {
        SaveData();
    }
        
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
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("HairSkin", hairSkin);
        PlayerPrefs.SetString("BodySkin", bodySkin);
        PlayerPrefs.SetInt("Gender", (int)gender);

        PlayerPrefs.SetInt("StoreMoney", storeMoney);
        PlayerPrefs.SetInt("StoreLevel", storeLevel);
    }

    public void LoadData()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        hairSkin = PlayerPrefs.GetString("HairSkin");
        bodySkin = PlayerPrefs.GetString("BodySkin");
        gender = (Gender)PlayerPrefs.GetInt("Gender");

        storeMoney = PlayerPrefs.GetInt("StoreMoney");
        storeLevel = PlayerPrefs.GetInt("StoreLevel");
    }
}
