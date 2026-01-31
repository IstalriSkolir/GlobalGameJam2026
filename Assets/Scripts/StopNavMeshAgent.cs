using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopNavMeshAgent : MonoBehaviour
{
    public NavMeshAgent agent;

    public void StopAgent()
    {
        agent.isStopped = false;
    }
}
