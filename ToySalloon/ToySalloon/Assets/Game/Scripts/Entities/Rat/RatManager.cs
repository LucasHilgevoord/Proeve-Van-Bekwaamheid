using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatManager : MonoBehaviour
{
    public delegate void SpawnedRat();
    public static event SpawnedRat OnSpawn;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Transform entities;
    [SerializeField] private GameObject ratPrefab;

    private float curRatTimer = 80;
    private float minTimer = 100;
    private float maxTimer = 150;

    private List<GameObject> rats = new List<GameObject>();
    private int maxRats = 1;

    void Update()
    {
        curRatTimer -= Time.deltaTime;
        if (curRatTimer < 0 && rats.Count < maxRats)
        {
            curRatTimer = Random.Range(minTimer, maxTimer);
            CreateRat();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateRat();
        }
    }

    /// <summary>
    /// Spawn a new rat.
    /// </summary>
    void CreateRat()
    {
        GameObject newRat = Instantiate(ratPrefab, entities);
        newRat.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        newRat.GetComponent<RatController>().player = player;
        rats.Add(newRat);
        OnSpawn();
    }
}
