using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APickup : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnPickup;

    public void Pickup()
    {
        OnPickup();

        if(destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
	
}
