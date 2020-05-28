using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteText : MonoBehaviour
{
    public delegate void Finish();
    public static event Finish OnFinished;

    [SerializeField] private float typeSpeed;

    public enum typingState { NORMAL, TYPING}
    public typingState type;

    public IEnumerator ShowMessage(string mes, TextMeshProUGUI box)
    {
        box.text = "";

        type = typingState.TYPING;

        foreach (char letter in mes.ToCharArray())
        {
            box.text += letter;

            if (type == typingState.NORMAL)
            {
                box.text = mes;
                OnFinished();
                yield break;
            }

            yield return new WaitForSeconds(typeSpeed);
        }

        type = typingState.NORMAL;

        OnFinished();
    }
}
