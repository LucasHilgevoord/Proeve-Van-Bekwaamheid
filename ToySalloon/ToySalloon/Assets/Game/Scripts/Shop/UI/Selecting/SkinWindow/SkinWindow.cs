using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinWindow : MonoBehaviour
{
    public delegate void ItemBought(int price);
    public static event ItemBought OnItemBought;

    [SerializeField] private SelectWindow selectWindow;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject item;

    private SelectableObject selectedObject;
    private GameObject obj;

    private List<SkinItem> items = new List<SkinItem>();
    private int currentSelected = 0;

    private void OnEnable()
    {
        FillScrollView();
        SkinItem.OnSkinSelect += SelectSkin;
    }
    private void OnDisable()
    {
        SkinItem.OnSkinSelect -= SelectSkin;
    }

    /// <summary>
    /// Create new items in the scrollview.
    /// </summary>
    private void FillScrollView()
    {
        if (obj != selectWindow.selectedObject)
        {
            // Refill the scroll view if the selected object
            // if it isn't the same as the previous one.
            ClearScrollView();

            // Fill the scrollview with items.
            for (int i = 0; i < selectedObject.skins.mySkins.Length; i++)
            {
                GameObject newItem = Instantiate(item, content.transform);
                SkinItem skin = newItem.GetComponent<SkinItem>();
                items.Add(skin);

                skin.SetImage(selectedObject.skins.mySkins[i].icon);
                skin.id = i;

                if (selectedObject.skins.mySkins[i].isSelected)
                {
                    currentSelected = i;
                    SelectSkin(skin);
                }
                UpdateVisuals(skin);
            }
        } else
        {
            // Else update all the items.
            for (int i = 0; i < items.Count; i++)
            {
                UpdateVisuals(items[i]);
            }
        }
    }

    /// <summary>
    /// Destroy all scrollview items.
    /// </summary>
    private void ClearScrollView()
    {
        obj = selectWindow.selectedObject;
        selectedObject = obj.GetComponent<SelectableObject>();

        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }
        items.Clear();
    }

    /// <summary>
    /// Update the locked visuals on the item.
    /// </summary>
    /// <param name="s"></param>
    private void UpdateVisuals(SkinItem s)
    {
        ObjectSkinLock locked = selectedObject.skins.mySkins[s.id].locked;
        if (locked.levelRequirement > GameManager.Instance.playerLevel)
        {
            s.SetLevelLock(locked.levelRequirement);
        } else if (locked.price != 0)
        {
            s.SetMoneyLock(locked.price);
        }
    }

    /// <summary>
    /// Select a new item and apply the new skin.
    /// </summary>
    /// <param name="s"></param>
    private void SelectSkin(SkinItem s)
    {
        ObjectSkin selected = selectedObject.skins.mySkins[s.id];

        // Check if item is bound to a price
        if (selected.locked.price > 0)
        {
            if (selected.locked.price > GameManager.Instance.storeMoney) return;

            s.DisableMoney();
            OnItemBought(-selectedObject.skins.mySkins[s.id].locked.price);
            selected.locked.price = 0;
        }

        // Disable previous visuals for selected.
        items[currentSelected].SetSelected(false);

        // Disable selected value in object skin list.
        selectedObject.skins.mySkins[currentSelected].isSelected = false;

        // Enable new visuals for selected.
        currentSelected = s.id;
        s.SetSelected(true);

        // Enable selected value in object skin list.
        selectedObject.skins.mySkins[s.id].isSelected = true;

        // Remove previous skin and create new skin.
        Destroy(selectedObject.modelParent.transform.GetChild(0).gameObject);
        Instantiate(selected.skin, selectedObject.modelParent.transform);
    }
}
