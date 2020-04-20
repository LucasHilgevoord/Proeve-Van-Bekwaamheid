using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject walkIcon;

    private void OnEnable()
    {
        InputManager.OnObjectClicked += OnFloorPressed;
    }
    private void OnDisable()
    {
        InputManager.OnObjectClicked -= OnFloorPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnFloorPressed(Transform t)
    {
        if (t.gameObject.tag == "Ground")
        {
            Vector3 newPos;
            Ray ray = Application.isEditor ? Camera.main.ScreenPointToRay(Input.mousePosition) : Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                newPos = hit.point;
                MovePlayerToPos(newPos);
            }
        }
    }

    public void MovePlayerToPos(Vector3 newPos)
    {
        agent.SetDestination(newPos);
        walkIcon.SetActive(true);
        walkIcon.transform.position = new Vector3(newPos.x, newPos.y + 0.1f, newPos.z);
    }
}
