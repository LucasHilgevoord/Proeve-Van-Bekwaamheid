using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public LayerMask draggableMask;

    private UnityEvent puzzleEvent;
    private GameObject selectedObject;
    private bool isDragging;

    void Start()
    {
        isDragging = false;

        if (puzzleEvent == null)
            puzzleEvent = new UnityEvent();

        puzzleEvent.AddListener(PuzzleCollide);
    }

    void Update()
    {
        DragPieces();

        if (Input.anyKeyDown && puzzleEvent != null)
        {
            puzzleEvent.Invoke();
        }
    }

    void DragPieces()
    {
        Debug.Log(isDragging);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);

            Debug.DrawLine(ray.origin, ray.direction, Color.red, Mathf.Infinity);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
                isDragging = true;
            }

            if (isDragging)
            {

                selectedObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void PuzzleCollide()
    {
        Debug.Log("Ping");
    }
}
