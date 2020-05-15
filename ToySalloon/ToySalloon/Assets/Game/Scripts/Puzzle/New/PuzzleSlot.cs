using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour,
    IDropHandler
{
    public Transform preview;

    [SerializeField]
    private bool field = true;

    [SerializeField]
    private HandlePuzzle puzzle;
    
    public void OnDrop(PointerEventData eventData)
    {
        if(field && eventData.pointerDrag.GetComponent<ItemDragHandler>() != null)
        {
            eventData.pointerDrag.transform.parent = preview.parent;
            preview.SetAsLastSibling();

            if (preview.parent.childCount - 1 == puzzle.puzzle.lines.Length)
            {
                preview.gameObject.SetActive(false);
            }
        }
        else
        {
            if(eventData.pointerDrag.GetComponent<ItemDragHandler>() != null)
            {
                eventData.pointerDrag.GetComponent<ItemDragHandler>().SnapBack();
                if (preview.parent.childCount - 1 < puzzle.puzzle.lines.Length)
                {
                    preview.gameObject.SetActive(true);
                }
            }
        }
    }
}
