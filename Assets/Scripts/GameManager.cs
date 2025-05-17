using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] items;
    public GameObject finishTrigger;

    private bool triggered = false;
    public GameObject chest;
    public GameObject chestTrigger;

    private void Start()
    {
        if (chest.activeInHierarchy)
        {
            Destroy(chestTrigger);
        }
    }
    void Update()
    {
        if (triggered) return;

        foreach (var item in items)
        {
            if (!item.activeInHierarchy)
            {
                return;
            }
        }
        finishTrigger.SetActive(true);
        triggered = true;
    }
}
