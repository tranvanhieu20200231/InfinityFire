using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float RollTime;
    bool rollOnce = false;

    private float rollBoost = 2f;
    private float rollTime;
    private Rigidbody rb;

    public SpriteRenderer characterSR;
    public Animator animator;
    public Vector3 moveInput;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
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

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && rollOnce == false)
        {
            moveSpeed += rollBoost;
            rollTime = RollTime;
            rollOnce = true;

            animator.SetBool("Roll", true);
        }

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
}
