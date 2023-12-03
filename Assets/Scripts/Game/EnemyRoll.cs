using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoll : MonoBehaviour
{
    public float RollTime;
    public float RollCD;
    public float rollBoost;

    private float rollCDT = 0;
    private float rollTime;
    private bool rollOnce = false;

    private Animator animator;
    public EnemyAI enemyAI;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        Roll();
    }

    void Roll()
    {
        if (rollOnce == false && rollCDT <= 0)
        {
            enemyAI.moveSpeed += rollBoost;
            rollTime = RollTime;
            rollOnce = true;
            rollCDT = RollCD;
            animator.SetBool("Roll", true);
        }
        rollCDT -= Time.deltaTime;

        if (rollTime <= 0 && rollOnce == true)
        {
            enemyAI.moveSpeed -= rollBoost;
            rollOnce = false;
            animator.SetBool("Roll", false);
        }
        else
        {
            rollTime -= Time.deltaTime;
        }
    }
}
