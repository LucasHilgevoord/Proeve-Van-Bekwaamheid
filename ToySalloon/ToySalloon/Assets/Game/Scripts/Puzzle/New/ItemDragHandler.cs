using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,
    IPointerDownHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler,
    IDropHandler
{
    private Transform originalParent;

    public Canvas canvas;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        Cursor.visible = false;
        originalParent = GetComponentInParent<ContentSizeFitter>().transform;
        canvas = GetComponentInParent<Image>().GetComponentInParent<ScrollRect>().GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Started dragging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        transform.parent = canvas.transform;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ended dragging");
        Cursor.visible = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        transform.parent = originalParent;
    }
}
