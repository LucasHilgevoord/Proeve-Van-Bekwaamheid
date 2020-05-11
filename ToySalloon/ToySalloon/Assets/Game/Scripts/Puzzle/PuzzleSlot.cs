using System;
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

    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("ParentHolder").transform, false);
        rect = GetComponent<RectTransform>();
        manager = GameObject.FindObjectOfType(typeof(PlaceHolderManager)) as PlaceHolderManager;
    }

    // Check when puzzle piece is in placeholder
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            manager.placedPieces.Add(eventData.pointerDrag);
            manager.puzzleOrder.Add(eventData.pointerDrag.GetComponent<PuzzleBehaviour>().puzzleNumber);

            if (manager.placedPieces.Count < 4 && manager.placeHolderCount < 3)
            {
                manager.SpawnPlaceHolder();
            }
        } 
    }
    
}
