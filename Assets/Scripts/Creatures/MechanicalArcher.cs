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
    private State state;

    [SerializeField, Header("Mechanical Archer Gameobjects & Components")]
    private GameObject childMesh;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private List<ParticleSystem> arrows;
    [SerializeField]
    private List<Transform> waypoints;

    //private float updateFloat;
    private float attackFloat;

    internal override void Start()
    {
        base.Start();
        //updateFloat = updateTimer;
        attackFloat = attackDelay;
    }

    void Update()
    {
        switch (state)
        {
            case State.Searching_For_Waypoint: searchForNewWaypoint(); break;
            case State.Retreating: retreating(); break;
            case State.Attack: attack(); break;
        }


        //updateFloat = Time.deltaTime;
        //if (updateFloat <= 0)
        //{
        //    updateFloat = updateTimer;
        //    switch (state)
        //    {
        //        case State.Searching_For_Waypoint: searchForNewWaypoint(); break;
        //        case State.Retreating: retreating(); break;
        //        case State.Attack: attack(); break;
        //    }
        //}
    }

    private void searchForNewWaypoint()
    {
        childMesh.transform.LookAt(transform.forward);
        Vector3 destination = getNewWaypoint();
        SetDestination(destination);
        state = State.Retreating;
    }

    private void retreating()
    {
        float distance = Vector3.Distance(destination, transform.position);
        if (distance <= distanceBuffer)
        {
            state = State.Attack;
            destination = new Vector3(0, 0, 0);
        }
    }

    private void attack()
    {
        childMesh.transform.LookAt(player.transform.position);
        attackFloat -= Time.deltaTime;
        if (attackFloat <= 0)
        {
            attackFloat = attackDelay;
            foreach(ParticleSystem system in arrows)
            {
                system.Emit(arrowPerAttackPerSystem);
            }
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
        Dictionary<float, Transform> waypointsByDistance = new Dictionary<float, Transform>();
        foreach(Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(player.transform.position, waypoint.position);
            if (!waypointsByDistance.ContainsKey(distance))
            {
                waypointsByDistance.Add(distance, waypoint);
            }
        }
        List<float> distances = waypointsByDistance.Keys.ToList();
        float nearestDistance = distances.Min();
        destination = waypointsByDistance[nearestDistance].position;
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
