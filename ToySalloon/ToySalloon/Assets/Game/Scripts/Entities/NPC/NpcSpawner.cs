using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    // Quick fix for agent avoider.
    private int priority = 1; // Priority of the NavMeshAgent avoidancePriority, 0 = player

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

            if (priority == 100)
            {
                priority = 1;
            }
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
        priority++;
        newNpc.GetComponent<NavMeshAgent>().avoidancePriority = priority;


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
