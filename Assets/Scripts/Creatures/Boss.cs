using UnityEngine;
using UnityEngine.AI;

public abstract class Boss : Creature
{
    //[SerializeField, Header("Boss Gameobjects & Components")]
    //private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    public virtual void SetDestination(Vector3 position)
    {
        //agent.SetDestination(position);
    }

    public virtual void PlayAnimationByTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
