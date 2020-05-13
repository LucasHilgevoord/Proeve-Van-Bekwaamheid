
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Rigidbody rb;

    //Speed Values
    private float followSpeed = 0.1f;
    private float dragSpeedTouch = 0.01f;
    private float dragSpeedMouse = 0.2f;
    private float zoomSpeedMouse = 5f;

    //Zoom Values
    private Vector3 startPosition;
    private Vector3 position;
    private float maxDistance = 5;
    private float minDistance = -5f;
    private float zoomSpeed = 1.5f;
    private float currentDistance;
    private float desiredDistance;

    [SerializeField]
    private Vector3 targetOffset;
    private bool isManual = true;

    private void OnEnable()
    {
        InputManager.OnObjectClicked += FollowTarget;
        InputManager.OnTouchHold += MoveManual;
        InputManager.OnMultiTouch += Zoom;
    }
    private void OnDisable()
    {
        InputManager.OnObjectClicked -= FollowTarget;
        InputManager.OnTouchHold -= MoveManual;
        InputManager.OnMultiTouch -= Zoom;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        //Follow the target
        if (!isManual)
        {
            Vector3 desiredPos = target.position + targetOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed);
        }
    }

    /// <summary>
    /// Enable user controlled camera movement
    /// </summary>
    private void MoveManual()
    {
        isManual = true;
        if (!Application.isMobilePlatform)
        {
            Vector3 desiredPos = transform.position - new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            transform.position = Vector3.Lerp(transform.position, desiredPos, dragSpeedMouse);
        }
        else
        {
            Touch t = Input.GetTouch(0);
            Vector3 desiredPos = transform.position - new Vector3(t.deltaPosition.x, 0, t.deltaPosition.y);
            transform.position = Vector3.Lerp(transform.position, desiredPos, dragSpeedTouch);
            startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// Enable user controlled camera zooming
    /// </summary>
    private void Zoom()
    {
        isManual = true;
        if (!Application.isMobilePlatform)
        {
            //Zoom in/out
            Vector3 newPos = transform.position;
            newPos.y += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeedMouse;
            newPos.z -= -Input.GetAxis("Mouse ScrollWheel") * zoomSpeedMouse;
            transform.position = newPos;
        }
        else
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

            desiredDistance += deltaMagDiff;
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomSpeed);

            position = (transform.rotation * Vector3.forward * -currentDistance);
            position = position + startPosition;
            transform.position = position;
        }
    }

    /// <summary>
    /// Follow specific object
    /// </summary>
    /// <param name="_target"></param>
    public void FollowTarget(Transform _target)
    {
        if (_target.gameObject.tag == "Entity" || _target.gameObject.tag == "Selectable")
        {
            target = _target;
            isManual = false;
        }
    }
}