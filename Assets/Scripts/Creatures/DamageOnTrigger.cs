using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public int damage = 5;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss") {
            other.GetComponent<Boss>().health -= damage;
            other.GetComponent<Boss>().UpdateHealthByValue(damage);
        }
    }
}
