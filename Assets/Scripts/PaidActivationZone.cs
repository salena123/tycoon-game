using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaidActivationZone : MonoBehaviour
{
    public int cost = 10;
    public GameObject[] objectsToActivate;
    public TMP_Text costTextUI;
    public string activationID = "Activation_Zone_1";

    private bool activated = false;

    void Start()
    {
        // Проверка сохранённого состояния
        activated = PlayerPrefs.GetInt(activationID, 0) == 1;

        if (activated)
        {
            ActivateObjects();
            UpdateCostUI("Активировано");
        }
        else
        {
            UpdateCostUI("Цена: " + cost);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;

        if (MoneyManager.money >= cost)
        {
            MoneyManager.AddMoney(-cost);

            activated = true;
            PlayerPrefs.SetInt(activationID, 1);
            PlayerPrefs.Save();

            ActivateObjects();
            UpdateCostUI("Активировано");
        }
        else
        {
            UpdateCostUI("Не хватает денег");
        }
    }

    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    void UpdateCostUI(string text)
    {
        if (costTextUI != null)
            costTextUI.text = text;
    }
}
