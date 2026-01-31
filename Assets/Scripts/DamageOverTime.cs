using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private CreatureType targetCreatures;
    [SerializeField]
    private float damageTick;
    [SerializeField]
    private float damage;
    [SerializeField]
    private List<Creature> creaturesInRange;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
