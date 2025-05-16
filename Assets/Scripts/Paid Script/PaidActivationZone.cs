using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaidActivationZone : MonoBehaviour
{
    [System.Serializable]
    public class ActivationStage
    {
        public int cost;
        public GameObject[] objectsToActivate;
    }

    public ActivationStage[] stages; // 3 стадии
    public TMP_Text costTextUI;
    public string activationID = "ActivationZone_1";
    public bool isConveuor;

    private int currentStage = 0;

    void Start()
    {
        currentStage = PlayerPrefs.GetInt(activationID + "_Stage", 0);

        // Активируем уже купленные стадии
        for (int i = 0; i < currentStage && i < stages.Length; i++)
        {
            ActivateObjects(stages[i].objectsToActivate);
        }

        UpdateCostUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (currentStage >= stages.Length) return;

        ActivationStage stage = stages[currentStage];

        if (MoneyManager.money >= stage.cost)
        {
            MoneyManager.AddMoney(-stage.cost);
            ActivateObjects(stage.objectsToActivate);

            currentStage++;
            PlayerPrefs.SetInt(activationID + "_Stage", currentStage);
            PlayerPrefs.Save();

            UpdateCostUI();
        }
        else
        {
            UpdateCostUI("Не хватает денег");
        }
    }

    void ActivateObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    void UpdateCostUI()
    {
        if (costTextUI == null) return;

        if (currentStage >= stages.Length)
        {
            if(!isConveuor)
            {
                Destroy(gameObject);
            }
            else
            {
                costTextUI.text = "Все активировано";
            }
        }
        else
        {
            costTextUI.text = "Цена: " + stages[currentStage].cost;
        }
    }

    void UpdateCostUI(string message)
    {
        if (costTextUI != null)
            costTextUI.text = message;
    }
}
