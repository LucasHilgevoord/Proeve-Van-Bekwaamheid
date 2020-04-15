using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint; // Position where the npc will be created.
    [SerializeField]
    private GameObject npcPrefab; // Link to npc prefab.
    [SerializeField]
    private AudioClip doorBell; // Audio which is played when npc's enters the store.
    [SerializeField]
    private Transform parent; // Object where the npc needs to be the child off.

    private GameObject newNpc;
    private AudioSource audioSrc;
    private WorldManager manager;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        manager = WorldManager.SharedInstance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && manager.npcs.Count < manager.maxCustomers)
        {
            SpawnNPC();
        }
    }

    /// <summary>
    /// Spawn npc on assigned spawnPoint;
    /// </summary>
    private void SpawnNPC()
    {
        //Creating new npc
        newNpc = Instantiate(npcPrefab, parent);
        newNpc.transform.position = spawnPoint.position;
        manager.npcs.Add(newNpc.GetComponent<NpcController>());

        //Playing door audio
        audioSrc.PlayOneShot(doorBell);
    }

    /// <summary>
    /// Fadout npc and remove from scene.
    /// </summary>
    private void RemoveNPC(GameObject npc)
    {
        audioSrc.PlayOneShot(doorBell);
        //Fadeout
        //Destroy
    }
}
