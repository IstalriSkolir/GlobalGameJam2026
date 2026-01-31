using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    [SerializeField]
    internal GameObject explosion;

    public UnityEvent m_MyEvent;

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
        m_MyEvent.Invoke();
    }

    public virtual void StopAgent()
    {
        agent.isStopped = true;
    }

    public virtual void StartAgent()
    {
        agent.isStopped = false;
    }
}
