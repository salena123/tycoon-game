using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed; // скорость, мен€ем в инспекторе
    public Vector3 direction; // направление, мен€ем в инспекторе

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            // ѕрибавл€ем движение к существующей скорости
            Vector3 conveyorVelocity = direction.normalized * speed;
            Vector3 newVelocity = conveyorVelocity;

            // —охран€ем вертикальную скорость (например, если объект падает)
            newVelocity.y = rb.velocity.y;

            rb.velocity = newVelocity;
        }
    }
}
