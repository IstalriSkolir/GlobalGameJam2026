using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private ParticleCollisionEffect mode;
    [SerializeField]
    private int value;

    [SerializeField, Header("GameObjects & Components")]
    private ParticleSystem system;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerController = player.GetComponent<PlayerController>();
    }

    public void EmitParticles(int particleCount)
    {
        system.Emit(particleCount);
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player" && !playerController.blocking)
        {
            switch (mode)
            {
                case ParticleCollisionEffect.Damage: damagePlayer(); break;
                case ParticleCollisionEffect.Slow: slowPlayer(); break;
            }
        }
    }

    private void slowPlayer()
    {

    }

    private void damagePlayer()
    {
        playerHealth.UpdateHealthByValue(value);
    }

    public enum ParticleCollisionEffect
    {
        Damage,
        Slow
    }
}
