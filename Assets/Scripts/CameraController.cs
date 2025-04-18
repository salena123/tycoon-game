using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;                // Объект, за которым следует камера
    public float distance = 10f;            // Желаемое расстояние от цели
    public float height = 2f;               // Высота, на которую направлен взгляд камеры
    public float rotationSpeed = 5f;        // Скорость вращения мышью
    public float minVerticalAngle = -30f;   // Минимальный угол камеры вниз
    public float maxVerticalAngle = 60f;    // Максимальный угол камеры вверх
    public LayerMask collisionMask;         // Слоёв, которые считаются препятствиями

    private float yaw = 0f;                 // Горизонтальный угол
    private float pitch = 20f;              // Вертикальный угол

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

        // Луч от цели к предполагаемой позиции камеры
        RaycastHit hit;
        if (Physics.Linecast(lookTarget, desiredCameraPos, out hit, collisionMask))
        {
            // Если есть препятствие — перемещаем камеру к точке столкновения
            desiredCameraPos = hit.point + hit.normal * 0.3f; // немного отступаем от поверхности
        }

        transform.position = desiredCameraPos;
        transform.LookAt(lookTarget);
    }
}
