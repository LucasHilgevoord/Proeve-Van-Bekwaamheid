using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleBehaviour : MonoBehaviour, 
    IPointerDownHandler, 
    IBeginDragHandler, 
    IEndDragHandler, 
    IDragHandler
{

    public int puzzleNumber;

    [SerializeField]
    private Canvas canvas; // Reference to the canvas

    [SerializeField, TextArea(1,2)]
    private string puzzleLine;

    private Text textBox;

    private CanvasGroup canvasGroup; // Reference to the canvas group on transform
    private RectTransform rectTransform; // Reference to transform with this script

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        textBox = GetComponentInChildren<Text>();
        textBox.text = puzzleLine;
    }

    // Checks the beginning of the object being dragged
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    // Checks whether the object is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Checks whether the object stopped being dragged
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    // Checks whether the object is being clickec
    public void OnPointerDown(PointerEventData eventData)
    {
        //CLICKED (USE IT FOR SHAKE ANIMATION)
    }
}
