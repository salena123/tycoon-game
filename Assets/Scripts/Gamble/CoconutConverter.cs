using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoconutConverter : MonoBehaviour
{
    public float currentRate = 1f;
    public float minRate = 0.5f;
    public float maxRate = 2f;

    public float rateUpdateInterval = 5f; // каждые 5 секунд
    private float rateTimer;

    private void Start()
    {
        UpdateRate();
        rateTimer = rateUpdateInterval;
    }

    private void Update()
    {
        rateTimer -= Time.deltaTime;

        if (rateTimer <= 0f)
        {
            UpdateRate();
            rateTimer = rateUpdateInterval;
        }
    }

    private void UpdateRate()
    {
        currentRate = Random.Range(minRate, maxRate);
    }

    public void ConvertCoconutsToMoney()
    {
        int coconuts = CoconutManager.coconuts;

        if (coconuts > 0)
        {
            int money = Mathf.RoundToInt(coconuts * currentRate);
            CoconutManager.SetCoconuts(0);
            MoneyManager.AddMoney(money);
        }
    }
}
