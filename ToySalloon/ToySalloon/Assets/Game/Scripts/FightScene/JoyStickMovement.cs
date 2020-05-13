using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float speed = 5.0f;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction * -1);
        }

    }
    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}