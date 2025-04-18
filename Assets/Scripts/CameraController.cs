using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;                // ������, �� ������� ������� ������
    public float distance = 10f;            // �������� ���������� �� ����
    public float height = 2f;               // ������, �� ������� ��������� ������ ������
    public float rotationSpeed = 5f;        // �������� �������� �����
    public float minVerticalAngle = -30f;   // ����������� ���� ������ ����
    public float maxVerticalAngle = 60f;    // ������������ ���� ������ �����
    public LayerMask collisionMask;         // ����, ������� ��������� �������������

    private float yaw = 0f;                 // �������������� ����
    private float pitch = 20f;              // ������������ ����

    void Start()
    {
        
    }

    void Update()
    {
        HandleCameraRotation();
        HandleCameraPosition();
    }

    void HandleCameraRotation()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
    }

    void HandleCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredCameraPos = target.position + rotation * new Vector3(0, 0, -distance);
        Vector3 lookTarget = target.position + Vector3.up * height;

        // ��� �� ���� � �������������� ������� ������
        RaycastHit hit;
        if (Physics.Linecast(lookTarget, desiredCameraPos, out hit, collisionMask))
        {
            // ���� ���� ����������� � ���������� ������ � ����� ������������
            desiredCameraPos = hit.point + hit.normal * 0.3f; // ������� ��������� �� �����������
        }

        transform.position = desiredCameraPos;
        transform.LookAt(lookTarget);
    }
}
