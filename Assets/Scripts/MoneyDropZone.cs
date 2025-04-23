using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDropZone : MonoBehaviour
{
    public int moneyInZone = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            moneyInZone += 1;
            Destroy(other.gameObject);
        }
    }
}
