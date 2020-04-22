using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPos;

    public GameObject placeHolder;

    public List<GameObject> placedPieces = new List<GameObject>();

    private Vector3 startPos;
    private float offset = 0;

    void Start()
    {
        startPos = prefabPos.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            offset += 160;
            createPlaceHolder(new Vector3(startPos.x, startPos.y - offset, startPos.z));
        }

        Debug.Log(placedPieces);
    }

    void createPlaceHolder(Vector3 spawnPosition)
    {
        Instantiate(placeHolder, spawnPosition, Quaternion.identity);
    }
}
