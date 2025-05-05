using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectorTerminal : MonoBehaviour
{
    public MoneyDropZone[] dropZones; // список всех зон

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int totalCollected = 0;

            foreach (MoneyDropZone zone in dropZones)
            {
                totalCollected += zone.moneyInZone;
                zone.moneyInZone = 0;
            }

            MoneyManager.AddMoney(totalCollected); // сохраняем сумму
        }
    }
}
