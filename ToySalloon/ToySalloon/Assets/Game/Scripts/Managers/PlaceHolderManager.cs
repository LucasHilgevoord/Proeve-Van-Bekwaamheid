using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPos;

    public GameObject placeHolder;

    public List<GameObject> placedPieces = new List<GameObject>();
    public List<int> puzzleOrder = new List<int>();

    private Vector3 startPos;
    private float offset = 0;

    void Start()
    {
        // Where the first placeholder should be put
        startPos = prefabPos.GetComponent<RectTransform>().anchoredPosition;
    }

    // Put placeholders on board
    public void SpawnPlaceHolder()
    {
        offset += 160;
        CreatePlaceHolder(new Vector3(startPos.x, startPos.y - offset, startPos.z));
    }

    // Create the placeholder
    void CreatePlaceHolder(Vector3 spawnPosition)
    {
        Instantiate(placeHolder, spawnPosition, Quaternion.identity);
    }
}
