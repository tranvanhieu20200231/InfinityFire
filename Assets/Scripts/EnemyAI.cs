using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Move")]
    public bool roaming = true;
    public bool updateContinuesPath;
    bool reachDestination = false;
    public float moveSpeed;
    public float nextWPDistance;
    public Seeker seeker;
    public SpriteRenderer characterSR;
    private Animator animator;

    Path path;

    Coroutine moveCoroutine;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;

        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }
            // Xoay enemy theo huong di chuyen
            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                }
                else
                {
                    characterSR.transform.localScale = new Vector3(1, 1, 0);
                }
            }

            yield return null;
        }

        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        // Xac dinh vi tri Player va di chuyen xung quanh
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        if (roaming == true)
        {
            return (Vector2)playerPos + (Random.Range(3f, 5f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
        }
        else
        {
            return (Vector2)playerPos + (Random.Range(0.5f, 1f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
        }
    }
}
