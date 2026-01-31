using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private CreatureType targetCreatures;
    [SerializeField]
    private float healthTick;
    [SerializeField]
    int healthChange;
    [SerializeField]
    private bool damageTargets;
    [SerializeField]
    private List<Creature> creaturesInRange;
    [SerializeField]
    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = healthTick;
            foreach (Creature creature in creaturesInRange)
                creature.UpdateHealthByValue(healthChange, damageTargets);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" && (targetCreatures == CreatureType.Boss || targetCreatures == CreatureType.All))
            creaturesInRange.Add(other.GetComponent<Creature>());
        else if (other.tag == "Player" && (targetCreatures == CreatureType.Player || targetCreatures == CreatureType.All))
            creaturesInRange.Add(other.GetComponent<Creature>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boss" && (targetCreatures == CreatureType.Boss || targetCreatures == CreatureType.All))
            creaturesInRange.Remove(other.GetComponent<Creature>());
        else if (other.tag == "Player" && (targetCreatures == CreatureType.Player || targetCreatures == CreatureType.All))
            creaturesInRange.Remove(other.GetComponent<Creature>());
    }
}
