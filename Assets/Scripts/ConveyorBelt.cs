using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed; // ��������, ������ � ����������
    public Vector3 direction; // �����������, ������ � ����������

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            // ���������� �������� � ������������ ��������
            Vector3 conveyorVelocity = direction.normalized * speed;
            Vector3 newVelocity = conveyorVelocity;

            // ��������� ������������ �������� (��������, ���� ������ ������)
            newVelocity.y = rb.velocity.y;

            rb.velocity = newVelocity;
        }
    }
}
