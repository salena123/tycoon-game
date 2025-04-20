using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MoneyManager.money += 1;

            Destroy(gameObject);
        }
    }
}
