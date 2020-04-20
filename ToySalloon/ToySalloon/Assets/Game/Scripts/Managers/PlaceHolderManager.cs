using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPos;

    public GameObject placeHolder;

    private PuzzleSlot puzzleSlot;
    private Vector3 startPos;
    private float offset = 0;

    void Start()
    {
        startPos = prefabPos.GetComponent<RectTransform>().anchoredPosition;
        puzzleSlot = GetComponent<PuzzleSlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            offset += 160; ;
            createPlaceHolder(new Vector3(startPos.x, startPos.y - offset, startPos.z));
        }
    }

    void createPlaceHolder(Vector3 spawnPosition)
    {
        Instantiate(placeHolder, spawnPosition, Quaternion.identity);
    }
}
