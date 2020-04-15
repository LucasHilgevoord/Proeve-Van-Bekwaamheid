using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTon
    private static GameManager instance = null;
    public static GameManager SharedInstance {
        get {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    #endregion

    [Header("Store data")]
    [SerializeField]
    public int storeMoney = 0; // The ammount of money the player has.
    [SerializeField]
    public int storeRating = 0; // Current rated stars of the store.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
