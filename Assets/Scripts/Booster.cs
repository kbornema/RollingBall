using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

    [SerializeField]
    private float force = 100.0f;

    void OnTriggerStay(Collider other)
    {
        Rigidbody otherBody = other.GetComponent<Rigidbody>();

        if(otherBody)
        {
            otherBody.AddForce(transform.right * force * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
