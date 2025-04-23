using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectorTerminal : MonoBehaviour
{
    public MoneyDropZone dropZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.money += dropZone.moneyInZone;
            dropZone.moneyInZone = 0;
        }
    }
}
