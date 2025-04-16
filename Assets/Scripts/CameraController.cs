using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // ������, �� ������� ������� ������
    public float distance = 10f;        // ���������� �� Z (������/�����)
    public float height = 4f;           // ���������� �� Y (�����/����)
    public float rotationSpeed = 100f;  // �������� �������� ������

    private float currentRotation;

    void Update()
    {
        
        currentRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);

        
        Vector3 offset = rotation * new Vector3(0, 0, -distance); 
        offset.y += height; 

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
