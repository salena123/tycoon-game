using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject ItemForSpawn;
    public GameObject spawnContainer;

    public void Spawning()
    {
        foreach (Transform child in spawnContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Vector3 spawningPosition = new Vector3(9f, 0.12f, 55.32f);
        GameObject spawnedItem = Instantiate(ItemForSpawn, spawningPosition, Quaternion.identity, spawnContainer.transform);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Spawning();
        }
    }
}
