using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowMe : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    Vector3 destination;

    private void Update()
    {
        destination = player.position;
        agent.destination = destination;
    }
}
