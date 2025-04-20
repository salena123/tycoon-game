using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{

    public GameObject objectPrefab; // ������ �������
    public float spawnInterval = 2.0f; // ������������� ����������
    public Transform spawnPoint; // �����, ��� ��������� �������

    private float timer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime; // �������� ������ �������
        if (timer >= spawnInterval) //���� ������ ����������� �������� �������, ������� ������ � �������� ������� �������
        {
            Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
            timer = 0f;
        }
    }
}
