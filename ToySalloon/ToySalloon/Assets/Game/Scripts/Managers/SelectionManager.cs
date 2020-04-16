using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Color selectionColor; // The color when the object is selected
    [SerializeField] private Color defaultColor; // The default color if the object is not selected
    [SerializeField] private LayerMask selectMask; // Which layer mask to check for raycast

    private Transform selectionField; // The selection field box

    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.rigidbody.gameObject.name);
        }

        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Infinity;

        // Check if something has been selected
        if (selectionField != null)
        {
            SpriteRenderer selectionRenderer = selectionField.GetComponent<SpriteRenderer>();
            selectionRenderer.color = defaultColor;
            selectionField = null;

        }

        // Cast a ray from mouse to screen on specific mask
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

        Debug.DrawRay(mousePosition, mousePosition - Camera.main.ScreenToWorldPoint(mousePosition), Color.blue);
        // Check if ray collider is empty or not
        if (hit.collider != null)
        {
            Debug.Log(hit.transform);
            Transform selection = hit.transform;
            SpriteRenderer selectionRenderer = selection.GetComponent<SpriteRenderer>();

            if (selectionRenderer != null)
            {
                selectionRenderer.color = selectionColor;
            }

            selectionField = selection;
        }
        */
    }
}
