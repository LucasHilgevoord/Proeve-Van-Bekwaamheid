using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private PlaceHolderManager manager;

    public bool checkIfEmpty = true;

    private RectTransform rect;

    // Check when this object or another object ha
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            manager.placedPieces.Add(eventData.pointerDrag);
        } 
    }

    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("ParentHolder").transform, false);
        rect = GetComponent<RectTransform>();
    }
}
