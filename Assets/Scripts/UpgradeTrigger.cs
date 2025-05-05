using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeTrigger : MonoBehaviour
{
    public int upgradeCost = 50;
    public GameObject[] objectsToActivate;
    public TMP_Text priceText;

    public string upgradeID = "Upgrade_1"; // Уникальный ID для PlayerPrefs

    public GameObject postUpgradeDialogObject; // ← сюда перетащи объект с новым диалогом

    private bool upgraded = false;

    void Start()
    {
        // Проверка, был ли апгрейд ранее
        upgraded = PlayerPrefs.GetInt(upgradeID, 0) == 1;

        if (upgraded)
        {
            ActivateObjects();
            UpdatePriceUI("Улучшено");
        }
        else
        {
            UpdatePriceUI("Цена: " + upgradeCost);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (upgraded) return;

        if (other.CompareTag("Player") && MoneyManager.money >= upgradeCost)
        {
            MoneyManager.AddMoney(-upgradeCost);

            upgraded = true;
            PlayerPrefs.SetInt(upgradeID, 1); // Сохраняем факт апгрейда
            PlayerPrefs.Save();

            ActivateObjects();
            UpdatePriceUI("Улучшено");
            if (postUpgradeDialogObject != null)
                postUpgradeDialogObject.SetActive(true);
            Destroy(gameObject); // ← удаляем триггер
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

    void UpdatePriceUI(string text)
    {
        if (priceText != null)
            priceText.text = text;
    }
}
