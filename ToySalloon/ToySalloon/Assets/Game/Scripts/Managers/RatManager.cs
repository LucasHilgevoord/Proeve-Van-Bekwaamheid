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

    //public List<GameObject> placedPieces = new List<GameObject>();
    //public List<int> puzzleOrder = new List<int>();

    private Vector3 startPos;
    //private float offset = 0;
    //public float placeHolderCount = 0;

    void Start()
    {
        // Where the first placeholder should be put
        startPos = ratSpawnPoint.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRat();
        }
    }

    // Put placeholders on board
    public void SpawnRat()
    {
        //offset += 160;
        CreateRat(new Vector3(startPos.x, startPos.y, startPos.z));
    }

    // Create the placeholder
    void CreateRat(Vector3 spawnPosition)
    {
        //placeHolderCount += 1;
        Instantiate(ratPrefab, spawnPosition, Quaternion.identity);
    }
}
