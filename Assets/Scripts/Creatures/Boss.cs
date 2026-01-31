using UnityEngine;
using UnityEngine.AI;

public abstract class Boss : Creature
{
    [SerializeField, Header("Boss Gameobjects & Components")]
    internal NavMeshAgent agent;
    [SerializeField]
    internal Animator animator;
    [SerializeField]
    internal AudioSource audio;
    [SerializeField]
    internal Rigidbody rigidbody;
    [SerializeField]
    internal GameObject player;

    internal PlayerHealth playerHealth;

    internal virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public virtual void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public virtual void PlayAnimationByTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    internal override void death()
    {
        animator.SetTrigger("Death");
    }
}
