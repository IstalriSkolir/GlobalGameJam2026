using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField, Header("Creature Properties")]
    private int health;
    [SerializeField]
    private int maxHealth;

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
