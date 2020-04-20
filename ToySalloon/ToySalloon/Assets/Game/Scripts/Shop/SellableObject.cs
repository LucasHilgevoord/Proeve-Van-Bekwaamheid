using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellableObject : MonoBehaviour
{
    private enum objectType // All types that can be chosen for the sellable objects
    {
        POTION,
        ARMOR,
        WEAPONS,
        PLUSHY,
        MAGIC
    }

    [SerializeField]
    private objectType type; // The type of the sellable object.

    public Image itemImage; // The image displayed in the buy window.
    public string itemName; // The name displayed in the buy window.
    public float price; // The price of the object.
}
