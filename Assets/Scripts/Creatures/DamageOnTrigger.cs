using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Object") {
            other.GetComponent<Creature>().UpdateHealthByValue(damage);
        }
    }
}
