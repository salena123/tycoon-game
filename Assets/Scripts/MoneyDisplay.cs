using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public MoneyDropZone dropZone;
    public Text moneyText; // если TMP, замени на: public TMP_Text moneyText;

    void Update()
    {
        moneyText.text = dropZone.moneyInZone.ToString();
    }
}
