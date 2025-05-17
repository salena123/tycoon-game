using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int reward;
    private HashSet<GameObject> triggeredBalls = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball") && !triggeredBalls.Contains(other.gameObject))
        {
            triggeredBalls.Add(other.gameObject);
            CoconutManager.AddCoconuts(reward);
        }
    }
}
