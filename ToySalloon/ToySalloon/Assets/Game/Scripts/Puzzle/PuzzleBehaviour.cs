using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleBehaviour : MonoBehaviour, 
    IPointerDownHandler, 
    IBeginDragHandler, 
    IEndDragHandler, 
    IDragHandler
{

    [SerializeField] private Canvas canvas; // Reference to the canvas

    private CanvasGroup canvasGroup; // Reference to the canvas group on transform
    private RectTransform rectTransform; // Reference to transform with this script

    public int puzzleNumber;

    // Gets called on awake
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    // Checks the beginning of the object being dragged
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    // Checks whether the object is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Checks whether the object stopped being dragged
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    // Checks whether the object is being clickec
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
}
