using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    private bool checkIfEmpty = true;

    // Check when this object or another object ha
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            checkIfEmpty = false;
        } 
    }

    void Update()
    {
        Debug.Log("IS THE PLACEHOLDER EMPTY? " + checkIfEmpty);
    }
}
