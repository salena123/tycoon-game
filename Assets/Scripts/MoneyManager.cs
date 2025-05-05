using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static int money = 0;
    public TMP_Text moneyText;

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Δενόγθ: " + money.ToString();
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }
}
