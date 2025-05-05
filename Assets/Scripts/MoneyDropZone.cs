using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDropZone : MonoBehaviour
{
    public int moneyInZone = 0;
    private const string MoneyKey = "MoneyInZone";

    void Start()
    {
        // Загружаем сохранённое значение при старте игры
        moneyInZone = PlayerPrefs.GetInt(MoneyKey, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            moneyInZone += 1;
            PlayerPrefs.SetInt(MoneyKey, moneyInZone); // Сохраняем новое значение
            PlayerPrefs.Save(); // Гарантируем сохранение
            Destroy(other.gameObject);
        }
    }
}
