using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField, Header("Creature Properties")]
    private CreatureType creatureType;
    [SerializeField]
    internal int health;
    [SerializeField]
    internal int maxHealth;

    public CreatureType CreatureType {  get { return creatureType; } }
    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }

    public virtual void UpdateHealthByValue(int change, bool decrease = true)
    {
        if (decrease)
        {
            health -= change;
            if (health <= 0)
                death();
        }
        else
        {
            health += change;
            if (health > maxHealth)
                health = maxHealth;
        }
    }

    internal abstract void death();
}
