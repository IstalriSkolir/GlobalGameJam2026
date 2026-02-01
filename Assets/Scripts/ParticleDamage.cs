using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [SerializeField, Header("GameObjects & Components")]
    private ParticleSystem system;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {

        }
    }

    public enum ParticleCollisionEffect
    {
        Damage,
        Slow
    }
}
