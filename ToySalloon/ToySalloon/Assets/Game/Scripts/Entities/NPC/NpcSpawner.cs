using DG.Tweening;
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

    private float fadeSpeed = 1;

    private GameObject newNpc;
    private AudioSource audioSrc;
    private WorldManager manager;

    private void OnEnable()
    {
        NpcGoLeave.OnNpcLeave += RemoveNPC;
    }
    private void OnDisable()
    {
        NpcGoLeave.OnNpcLeave -= RemoveNPC;
    }

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
        NpcController c = newNpc.GetComponent<NpcController>();
        c.purpose = (NpcGoals)Random.Range(0, 2);
        manager.npcs.Add(c);

        //Playing door audio
        audioSrc.PlayOneShot(doorBell);
    }

    /// <summary>
    /// Fadout npc and remove from scene.
    /// </summary>
    private void RemoveNPC(NpcController npc)
    {
        SpriteRenderer npcRend = npc.gameObject.GetComponent<SpriteRenderer>();
        float alpha = 1f;
    
        DOTween.To(() => alpha, f => alpha = f, 0f, fadeSpeed).OnUpdate(() =>
        {
            //npc.Skeleton.SetColor(new Color(1, 1, 1, alpha));
            npcRend.color = new Color(1, 1, 1, alpha);
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            audioSrc.PlayOneShot(doorBell);
            manager.npcs.Remove(npc);
            Destroy(npc.gameObject);
        });
    }
}