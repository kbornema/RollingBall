using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Taker : MonoBehaviour 
{


    private void OnTriggerEnter(Collider other)
    {
        APickup pickup = other.GetComponent<APickup>();

        if(pickup)
        {
            pickup.Pickup();
        }
    }
	
}
