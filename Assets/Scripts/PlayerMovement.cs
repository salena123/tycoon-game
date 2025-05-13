using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float rotationSpeed = 720f;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public float jumpCooldown = 2f;
    private float lastJumpTime = -Mathf.Infinity;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Camera")]
    public Transform cameraTransform;

    private Rigidbody rb;
    private Animator animator;

    private bool jumpQueued = false;

    private bool isGrounded;
    private Vector3 currentVelocity;
    private Vector3 smoothMoveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        // ѕоворачиваемс€ к направлению движени€
        if (inputDir.magnitude >= 0.1f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // ”скорение и замедление
            Vector3 targetVelocity = moveDir * moveSpeed;
            currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref smoothMoveVelocity, 0.1f);

            rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

            // јнимации
            animator.SetInteger("move", Input.GetKey(KeyCode.LeftShift) ? 2 : 1); // 2 Ч бег, 1 Ч ходьба
            animator.SetFloat("speed", Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f); // скорость анимации
        }
        else
        {
            // ќстановка
            currentVelocity = Vector3.SmoothDamp(currentVelocity, Vector3.zero, ref smoothMoveVelocity, 0.1f);
            rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

            // јнимации
            animator.SetInteger("move", 0); // 0 Ч idle
            animator.SetFloat("speed", 1f);
        }

        // ѕоворот головы в зависимости от A/D
        if (Input.GetKey(KeyCode.A))
            animator.SetInteger("head_turn", 1); // влево
        else if (Input.GetKey(KeyCode.D))
            animator.SetInteger("head_turn", 2); // вправо
        else
            animator.SetInteger("head_turn", 0); // пр€мо
    }

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckDistance, groundMask);

        if (isGrounded && Input.GetButtonDown("Jump") && Time.time >= lastJumpTime + jumpCooldown)
        {
            lastJumpTime = Time.time;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }
    }

    // Ётот метод вызываетс€ из ивента анимации Jump
    public void DoJump()
    {
        if (!isGrounded) return;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // сбрасываем Y
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpQueued = false;
    }
}
