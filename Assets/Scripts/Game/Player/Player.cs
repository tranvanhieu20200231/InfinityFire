using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float RollTime;
    public float rollBoost;
    public float RollCD;
    public bool rollOnce = false;

    private float rollCDT = 0;
    private float rollTime;

    public CDTimeBar CDTBar;
    public SpriteRenderer characterSR;
    private Animator animator;
    private Vector3 moveInput;
    private bool rollButton = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        CDTBar.UpdateCDTimeBar(rollCDT, RollCD);
        Time.timeScale = 1;
    }

    private void Update()
    {
        Move();
        Roll();
    }

    void Move()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput.x * moveInput.y != 0)
        {
            // Toc do di theo duong cheo
            transform.position += moveInput * Time.deltaTime * moveSpeed / Mathf.Sqrt(2);
        }
        else
        {
            // Toc do di theo duong thang
            transform.position += moveInput * Time.deltaTime * moveSpeed;
        }

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                characterSR.transform.localScale = new Vector3(1, 1, 0);
            else
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    public void RollButtonClicked()
    {
        rollButton = true;
    }

    void Roll()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || rollButton) && rollOnce == false && rollCDT <= 0)
        {
            moveSpeed += rollBoost;
            rollTime = RollTime;
            rollOnce = true;
            rollCDT = RollCD;
            rollButton = false;

            animator.SetBool("Roll", true);
        }
        rollCDT -= Time.deltaTime;
        CDTBar.UpdateCDTimeBar(rollCDT, RollCD);

        if (rollTime <= 0 && rollOnce == true)
        {
            moveSpeed -= rollBoost;
            rollOnce = false;

            animator.SetBool("Roll", false);
        }
        else
        {
            rollTime -= Time.deltaTime;
        }
    }

    public void ReduceRollCDT(float amount)
    {
        rollCDT -= amount;
        CDTBar.UpdateCDTimeBar(rollCDT, RollCD);
    }
}
