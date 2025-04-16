using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_manager : MonoBehaviour
{
    public bool pressed = false;
    public GameObject buildingForSpawn;
    public GameObject sprite;
    public int price;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Spawning(Collision collision)
    {
        
        if (collision.relativeVelocity.y < -1)
        {
            if (money_manager.money >= price) //(money_manager.money >= item1.price)
            {
                Vector3 spawningPosition = new Vector3(2, 2, 2);
                Instantiate(buildingForSpawn, spawningPosition, Quaternion.identity);
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 0.15f, sprite.transform.localScale.z);
                pressed = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pressed)
        {
            Spawning(collision);
        }
    }
}
