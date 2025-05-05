using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public MoneyDropZone[] dropZones;
    public TMP_Text moneyText;

    void Update()
    {
        int total = 0;

        foreach (MoneyDropZone zone in dropZones)
        {
            if (zone != null)
                total += zone.moneyInZone;
        }

        moneyText.text = total.ToString();
    }
}
