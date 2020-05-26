using DG.Tweening;
using Spine.Unity;
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
    private float npcMaxTimer = 10f;
    private float npcMinTimer = 20f;
    private float curNpcTimer = 0f;
    private bool canSpawn = true;

    private GameObject newNpc;
    private AudioSource audioSrc;
    private WorldManager manager;

    private void OnEnable()
    {
        NpcGoLeave.OnNpcLeave += RemoveNPC;
        RatManager.OnSpawn += ScareNPC;
        RatController.OnCaught += AllowSpawning;
    }
    private void OnDisable()
    {
        NpcGoLeave.OnNpcLeave -= RemoveNPC;
        RatManager.OnSpawn -= ScareNPC;
        RatController.OnCaught -= AllowSpawning;
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        manager = WorldManager.Instance;

        curNpcTimer = Random.Range(npcMinTimer, npcMaxTimer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && manager.npcs.Count < manager.maxCustomers)
        {
            SpawnNPC();
        }

        curNpcTimer -= Time.deltaTime;
        if (curNpcTimer < 0)
        {
            curNpcTimer = Random.Range(npcMinTimer, npcMaxTimer);
            if (canSpawn && manager.npcs.Count < manager.maxCustomers)
            {
                SpawnNPC();
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
        NpcController c = newNpc.GetComponent<NpcController>();

        //c.purpose = (NpcGoals)Random.Range(0, 2);
        //Quick fix so there won't spawn as many repair customers.
        int random = Random.Range(0, 100);
        if (random > 70)
        {
            c.purpose = NpcGoals.REPAIR;
        } else
        {
            c.purpose = NpcGoals.BUY;
        }
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
        NpcAnimator skel = npc.gameObject.GetComponent<NpcAnimator>();
        float alpha = 1f;
    
        DOTween.To(() => alpha, f => alpha = f, 0f, fadeSpeed).OnUpdate(() =>
        {
            skel.body.Skeleton.SetColor(new Color(1, 1, 1, alpha));
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            audioSrc.PlayOneShot(doorBell);
            manager.npcs.Remove(npc);
            Destroy(npc.gameObject);
        });
    }

    /// <summary>
    /// Move every npc to the exit.
    /// </summary>
    private void ScareNPC()
    {
        canSpawn = false;
        foreach (NpcController npc in manager.npcs)
        {
            Debug.Log("RUN");
            npc.ChangeState(NpcStates.LEAVE);
        }
    }

    private void AllowSpawning()
    {
        canSpawn = true;
    }
}