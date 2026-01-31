using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnColliderHit : MonoBehaviour
{
    public float forceToAdd = 50.0f;
    void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forceToAdd);
        }
    }
}
