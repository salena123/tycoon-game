using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaidItem : MonoBehaviour
{
    public int cost;                    
    public GameObject objectToActivate; 
    public TMP_Text costTextUI;          
    public string activationID = "ActivationZone_";

    private bool isActivated = false;

    void Start()
    {
        isActivated = PlayerPrefs.GetInt(activationID + "_Activated", 0) == 1;

        if (isActivated)
        {
            ActivateObject();
            UpdateCostUI("Уже куплено");
        }
        else
        {
            UpdateCostUI("Цена: " + cost);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isActivated) return;

        if (MoneyManager.money >= cost)
        {
            MoneyManager.AddMoney(-cost);
            ActivateObject();

            isActivated = true;
            PlayerPrefs.SetInt(activationID + "_Activated", 1);
            PlayerPrefs.Save();

            UpdateCostUI("Куплено");
        }
        else
        {
            UpdateCostUI("Не хватает денег");
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
            objectToActivate.SetActive(true);
    }

    void UpdateCostUI(string message)
    {
        if (costTextUI != null)
            costTextUI.text = message;
    }
}
