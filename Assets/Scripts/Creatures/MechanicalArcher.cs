using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MechanicalArcher : Boss
{
    [SerializeField, Header("Mechanical Archer Properties")]
    //private float updateTimer;
    //[SerializeField]
    private float distanceBuffer;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private int arrowPerAttackPerSystem;
    [SerializeField]
    private float attackTimeBeforeRetreat;
    [SerializeField]
    private float attackTimeRemaining;
    [SerializeField]
    private State state;

    [SerializeField, Header("Mechanical Archer Gameobjects & Components")]
    private GameObject childMesh;
    [SerializeField]
    private float smoothRotationSpeed;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private List<ParticleSystem> arrows;
    [SerializeField]
    private List<Transform> waypoints;

    private float attackFloat;
    bool attacking = false;

    internal override void Start()
    {
        base.Start();
        attackFloat = attackDelay;
        attackTimeRemaining = attackTimeBeforeRetreat;
    }

    void Update()
    {
        switch (state)
        {
            case State.Searching_For_Waypoint: searchForNewWaypoint(); break;
            case State.Retreating: retreating(); break;
            case State.Attack: attack(); break;
        }

        var step = smoothRotationSpeed * Time.deltaTime;

        if (!attacking) {
            Quaternion rotationTarget = Quaternion.LookRotation(transform.forward);
            childMesh.transform.rotation = Quaternion.RotateTowards(childMesh.transform.rotation, rotationTarget, step);

            //Vector3 lookDirection = lookAtTarget.position - mainCamera.position;
            //lookDirection.Normalize();

            //childMesh.transform.rotation = Quaternion.Slerp(childMesh.transform.rotation, Quaternion.LookRotation(transform.forward), smoothRotationSpeed * Time.deltaTime);
        }
        else{
            Quaternion rotationTarget = Quaternion.LookRotation(player.transform.position);
            childMesh.transform.rotation = Quaternion.RotateTowards(childMesh.transform.rotation, rotationTarget, step);

            //childMesh.transform.rotation = Quaternion.Slerp(childMesh.transform.rotation, Quaternion.LookRotation(player.transform.position), smoothRotationSpeed * Time.deltaTime);
        }

        
    }

    private void searchForNewWaypoint()
    {
        
        
        //childMesh.transform.LookAt(transform.forward);
        attackTimeRemaining = attackTimeBeforeRetreat;
        Vector3 destination = getNewWaypoint();
        SetDestination(destination);
        state = State.Retreating;
    }

    private void retreating()
    {
        float distance = Vector3.Distance(destination, transform.position);
        animator.SetBool("Attacking", false);
        attacking = false;
        if (distance <= distanceBuffer)
        {
            state = State.Attack;
            destination = new Vector3(0, 0, 0);
        }
    }

    private void attack()
    {
        //childMesh.transform.LookAt(player.transform.position);
        animator.SetBool("Attacking", true);
        attacking = true;
        attackFloat -= Time.deltaTime;
        attackTimeRemaining -= Time.deltaTime;
        if (attackFloat <= 0)
        {
            attackFloat = attackDelay;
            foreach(ParticleSystem system in arrows)
            {
                system.Emit(arrowPerAttackPerSystem);
            }
        }
        if(attackTimeRemaining <= 0)
        {
            searchForNewWaypoint();
        }
    }

    #region Utilities

    public override void UpdateHealthByValue(int change, bool decrease = true)
    {
        base.UpdateHealthByValue(change, decrease);
        state = State.Searching_For_Waypoint;
    }

    private Vector3 getNewWaypoint()
    {
        float highestDistance = float.MinValue;
        foreach(Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(player.transform.position, waypoint.position);
            if (distance > highestDistance)
            {
                highestDistance = distance;
                destination = waypoint.position;
            }
        }
        return destination;
    }

    private enum State
    {
        Searching_For_Waypoint,
        Retreating,
        Attack
    }

    #endregion
}
