using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoconutExchangeZone : MonoBehaviour
{
    public GameObject exchangeUI; // панель обмена
    public TMP_Text rateText;
    public TMP_Text coconutText;
    public TMP_Text moneyText;

    public CoconutConverter converter;

    private bool playerInZone = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            ShowExchangeUI();
        }
    }

    void Update()
    {
        if (playerInZone)
        {
            UpdateUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            HideExchangeUI();
        }
    }

    void ShowExchangeUI()
    {
        if (exchangeUI)
        {
            exchangeUI.SetActive(true);
            UpdateUI();
        }
    }

    void HideExchangeUI()
    {
        if (exchangeUI)
        {
            exchangeUI.SetActive(false);
        }
    }

    public void OnExchangeButtonClicked()
    {
        if (converter)
        {
            converter.ConvertCoconutsToMoney();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (converter)
        {
            rateText.text = "Курс: " + converter.currentRate.ToString("F2");
            coconutText.text = "Кокосы: " + CoconutManager.coconuts;
            moneyText.text = "Деньги: " + MoneyManager.money;
        }
    }
}
