using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static int money = 0;
    public float earnInterval = 1f;
    private float timer = 0f;
    public TMP_Text moneyText;

    void Start()
    {
        
    }

    void Update()
    {

        moneyText.text = "Δενόγθ: " + money.ToString();
    }

}
