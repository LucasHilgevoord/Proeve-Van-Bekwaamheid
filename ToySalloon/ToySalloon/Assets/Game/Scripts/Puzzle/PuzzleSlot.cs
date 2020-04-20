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
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            checkIfEmpty = false;
        } 
    }

    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("ParentHolder").transform, false);
    }
}
