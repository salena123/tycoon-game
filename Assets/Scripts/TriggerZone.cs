using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject dialogUI; // UI � ��������
    public GameObject objectToSpawn; // ������, ������� ��������
    public Transform spawnPoint; // ��� ��������
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
