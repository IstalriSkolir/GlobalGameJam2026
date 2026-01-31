using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MechanicalArcher : Boss
{
    [SerializeField, Header("Mechanical Archer Properties")]
    private float updateTimer;
    [SerializeField]
    private float distanceBuffer;
    [SerializeField]
    private float attackDelay;
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

    private float updateFloat;
    private attackFloat;

    internal override void Start()
    {
        base.Start();
    }

    void Update()
    {
        //switch (state)
        //{
        //    case State.Retreating: retreating(); break;
        //    case State.Standard_Attack: standardAttack(); break;
        //}


        updateFloat = Time.deltaTime;
        if (updateFloat <= 0)
        {
            updateFloat = updateTimer;
            switch (state)
            {
                case State.Searching_For_Waypoint: searchForNewWaypoint(); break;
                case State.Retreating: retreating(); break;
                case State.Attack: attack(); break;
            }
        }
    }

    private void searchForNewWaypoint()
    {
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
