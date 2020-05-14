using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Sellable", menuName = "Sellables")]
public class SellableObject : ScriptableObject
{
    private enum objectType // All types that can be chosen for the sellable objects
    {
        POTION,
        ARMOR,
        WEAPONS,
        PLUSHY,
        MAGIC,
        ANIMAL
    }

    [SerializeField]
    private objectType type; // The type of the sellable object.

    public Sprite itemImage; // The image displayed in the buy window.
    public int price; // The price of the object.
}
