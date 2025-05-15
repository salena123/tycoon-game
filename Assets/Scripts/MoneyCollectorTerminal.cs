using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectorTerminal : MonoBehaviour
{
    public MoneyDropZone[] dropZones;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int totalCoconuts = 0;

            foreach (MoneyDropZone zone in dropZones)
            {
                totalCoconuts += zone.moneyInZone;
                zone.moneyInZone = 0;
            }

            CoconutManager.AddCoconuts(totalCoconuts); // сохраняем как кокосы
        }
    }
}
