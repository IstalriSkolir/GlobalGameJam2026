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
    private bool isAlive;
    [SerializeField]
    private float forceOnDeath;
    [SerializeField]
    private float deleteDelayAfterDeath;
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
    [SerializeField]
    private List<GameObject> childrenWithMeshes;
    [SerializeField]
    private GameObject teleporter;

    private float attackFloat;
    bool attacking = false;

    internal override void Start()
    {
        base.Start();
        attackFloat = attackDelay;
        attackTimeRemaining = attackTimeBeforeRetreat;
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            switch (state)
            {
                case State.Searching_For_Waypoint: searchForNewWaypoint(); break;
                case State.Retreating: retreating(); break;
                case State.Attack: attack(); break;
            }

            //var step = smoothRotationSpeed * Time.deltaTime;

            childMesh.transform.LookAt(player.transform.position);
        }

        //if (!attacking) {
        //    Quaternion rotationTarget = Quaternion.LookRotation(transform.forward);
        //    childMesh.transform.rotation = Quaternion.RotateTowards(childMesh.transform.rotation, rotationTarget, step);

        //    //Vector3 lookDirection = lookAtTarget.position - mainCamera.position;
        //    //lookDirection.Normalize();

        //    //childMesh.transform.rotation = Quaternion.Slerp(childMesh.transform.rotation, Quaternion.LookRotation(transform.forward), smoothRotationSpeed * Time.deltaTime);
        //}
        //else{
        //    Quaternion rotationTarget = Quaternion.LookRotation(player.transform.position);
        //    childMesh.transform.rotation = Quaternion.RotateTowards(childMesh.transform.rotation, rotationTarget, step);

        //    //childMesh.transform.rotation = Quaternion.Slerp(childMesh.transform.rotation, Quaternion.LookRotation(player.transform.position), smoothRotationSpeed * Time.deltaTime);
        //}


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

    public void SetPortalReference(GameObject portal)
    {
        teleporter = portal;
    }

    public void SetWaypoints(List<Transform> waypoints)
    {
        this.waypoints = waypoints;
    }

    internal override void death()
    {
        isAlive = false;
        agent.isStopped = true;
        Destroy(animator);
        List<Vector3> directions = new List<Vector3>()
        {
            transform.forward,
            transform.up,
            transform.right,
            -transform.forward,
            -transform.up,
            -transform.right,
        };
        foreach(GameObject child in childrenWithMeshes)
        {
            child.transform.SetParent(transform);
            MeshCollider collider = child.AddComponent<MeshCollider>();
            collider.convex = true;
            Rigidbody body = child.AddComponent<Rigidbody>();
            Vector3 direction = directions[Random.Range(0, directions.Count)];
            body.AddForce(direction * forceOnDeath);
        }
        foreach(ParticleSystem system in arrows)
        {
            system.Stop();
        }
        teleporter.SetActive(true);
        Invoke("selfDestruct", deleteDelayAfterDeath);
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
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
