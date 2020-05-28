using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClosetItem : MonoBehaviour
{
    private Button btn;
    [SerializeField] private UnityEvent onClick;

    private void Start()
    {
        btn = GetComponent<Button>();
    }

    public void OnButtonClick()
    {
        onClick.Invoke();
    }
}
