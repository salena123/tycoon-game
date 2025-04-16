using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public float jumpForce = 7f;               // ���� ������
    public Transform cameraTransform;
    public LayerMask groundMask;               // ��� �������� �����
    public float groundCheckDistance = 0.2f;   // ���������� ��� �������� �����
    public Transform groundCheckPoint;         // �����, ������ ��������� �����
    public float jumpCooldown = 2f;  // �������� ����� ��������
    private float lastJumpTime = -Mathf.Infinity; // ����� ���������� ������
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
        HandleJump();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            Vector3 move = moveDirection * speed;
            Vector3 velocity = new Vector3(move.x, rb.velocity.y, move.z);
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckDistance, groundMask);

        // ��������: �� ����� � ������ ���������� ������� � ���������� ������
        if (isGrounded && Input.GetButtonDown("Jump") && Time.time >= lastJumpTime + jumpCooldown)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time; // ��������� ����� ���������� ������
        }
    }
}
