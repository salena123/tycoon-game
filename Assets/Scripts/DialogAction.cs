using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAction : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public GameObject dialogCanvas;

    public void OnDialogConfirmed()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        dialogCanvas.SetActive(false);
    }
}
