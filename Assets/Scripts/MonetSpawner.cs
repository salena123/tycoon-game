using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{

    public GameObject objectPrefab; // Префаб монетки
    public float spawnInterval = 2.0f; // Интенсивность создавания
    public Transform spawnPoint; // Точка, где создаются объекты

    private float timer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime; // Начинаем отсчет времени
        if (timer >= spawnInterval) //Если прошел назначенный интервал времени, создаем объект и обнуляем счетчик времени
        {
            Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
            timer = 0f;
        }
    }
}
