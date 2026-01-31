using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss") {
            other.GetComponent<Boss>().health -= damage;
            other.GetComponent<Boss>().UpdateHealthByValue(damage);
        }
    }
}
