
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
    private float zoomSpeedTouch = 0.001f;
    private float zoomSpeedMouse = 5f;

    [SerializeField]
    private Vector3 targetOffset;
    private bool isManual;

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
        FollowTarget(target);
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
        if (Application.isEditor)
        {
            Vector3 desiredPos = transform.position - new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            transform.position = Vector3.Lerp(transform.position, desiredPos, dragSpeedMouse);
        }
        else
        {
            Touch t = Input.GetTouch(0);
            Vector3 desiredPos = transform.position - new Vector3(t.deltaPosition.x, 0, t.deltaPosition.y);
            transform.position = Vector3.Lerp(transform.position, desiredPos, dragSpeedTouch);
        }
    }

    /// <summary>
    /// Enable user controlled camera zooming
    /// </summary>
    private void Zoom()
    {
        isManual = true;
        if (Application.isEditor)
        {
            //Zoom in/out
            Vector3 newPos = transform.position;
            newPos.y += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeedMouse;
            newPos.z -= -Input.GetAxis("Mouse ScrollWheel") * zoomSpeedMouse;
            transform.position = newPos;
        } else
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPos = secondTouch.position - secondTouch.deltaPosition;
            float prevMag = (firstTouchPos - secondTouchPos).magnitude;
            float curMag = (firstTouch.position - secondTouch.position).magnitude;
            float difference = curMag - prevMag * zoomSpeedTouch;
        }
    }

    /// <summary>
    /// Follow specific object
    /// </summary>
    /// <param name="_target"></param>
    public void FollowTarget(Transform _target)
    {
        if (_target.gameObject.tag == "Entity")
        {
            target = _target;
            isManual = false;
        }
    }
}
