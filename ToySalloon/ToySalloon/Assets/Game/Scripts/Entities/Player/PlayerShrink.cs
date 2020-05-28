using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerShrink : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent playerAgent;
    [SerializeField] Transform body;

    private float shrinkSize = 0.35f;
    private float shrinkSpeed = 1.2f;

    private void OnEnable()
    {
        RatManager.OnSpawn += OnShock;
        RatController.OnCaught += OnShrink;
    }
    private void OnDisable()
    {
        RatManager.OnSpawn -= OnShock;
        RatController.OnCaught -= OnShrink;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();
    }

    private void OnShrink()
    {
        anim.SetBool("isShrinking", true);
        playerAgent.destination = transform.position;

        // Shrinking object is done through script, the rest is animation!
        float scale = body.localScale.x;
        DOTween.To(() => scale, f => scale = f, shrinkSize, shrinkSpeed).OnUpdate(() =>
        {
            body.localScale = new Vector3(scale, scale, scale);
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            SceneManager.Instance.FadeToScene(3, 1f);
        });
    }

    private void OnShock()
    {
        anim.SetBool("isShocked", true);
    }
}
