using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    // Where the rat spawns
    [SerializeField]
    private GameObject ratSpawnPoint;

    // The rat prefab
    public GameObject ratPrefab;

    private Vector3 startPos;

    void Start()
    {
        // Where the first placeholder should be put
        startPos = ratSpawnPoint.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateRat(new Vector3(startPos.x, startPos.y, startPos.z));
        }
       
    }

    // Create the rat
    void CreateRat(Vector3 spawnPosition)
    {
        Instantiate(ratPrefab, spawnPosition, Quaternion.identity);
    }
}
