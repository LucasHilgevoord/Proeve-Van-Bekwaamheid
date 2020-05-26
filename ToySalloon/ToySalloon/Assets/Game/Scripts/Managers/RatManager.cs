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

    public List<GameObject> ratAmount = new List<GameObject>();
    private float curRatTimer = 80;
    private float ratMinTimer = 100;
    private float ratMaxTimer = 150;
    private int maxRats = 1;

    void Start()
    {
        // Where the first placeholder should be put
        startPos = ratSpawnPoint.transform.position;
    }

    void Update()
    {
        curRatTimer -= Time.deltaTime;
        if (curRatTimer < 0 && ratAmount.Count < maxRats)
        {
            curRatTimer = Random.Range(ratMinTimer, ratMaxTimer);

            CreateRat(new Vector3(startPos.x, startPos.y, startPos.z));
        }

        Debug.Log(ratAmount.Count);
        Debug.Log(curRatTimer);

    }

    // Create the rat
    void CreateRat(Vector3 spawnPosition)
    {
        ratAmount.Add(Instantiate(ratPrefab, spawnPosition, Quaternion.identity));
    }
}
