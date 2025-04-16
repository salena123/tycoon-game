using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // Объект, за которым следует камера
    public float distance = 10f;        // Расстояние по Z (вперед/назад)
    public float height = 4f;           // Расстояние по Y (вверх/вниз)
    public float rotationSpeed = 100f;  // Скорость вращения камеры

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
