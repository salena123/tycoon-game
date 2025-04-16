using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject dialogUI; // UI с диалогом
    public GameObject objectToSpawn; // Объект, который появится
    public Transform spawnPoint; // Где спавнить
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogUI.SetActive(true);
        }
    }
}
