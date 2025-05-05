using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDropZone : MonoBehaviour
{
    public int moneyInZone = 0;
    private const string MoneyKey = "MoneyInZone";

    void Start()
    {
        // ��������� ���������� �������� ��� ������ ����
        moneyInZone = PlayerPrefs.GetInt(MoneyKey, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            moneyInZone += 1;
            PlayerPrefs.SetInt(MoneyKey, moneyInZone); // ��������� ����� ��������
            PlayerPrefs.Save(); // ����������� ����������
            Destroy(other.gameObject);
        }
    }
}
