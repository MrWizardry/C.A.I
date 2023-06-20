using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Patrol_Route : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Transform[] patrolWaypoints; // Array of patrol waypoints
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private int currentWaypointIndex = 0;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        SetPatrolDestination();
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // Check if the agent has reached the current patrol waypoint
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                SetPatrolDestination();
            }
        }
    }

    private void SetPatrolDestination()
    {
        // Set the agent's destination to the next patrol waypoint
        agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);

        // Increment the waypoint index for the next destination
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowingPlayer = true;
            SceneManager.LoadScene("Menu");
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowingPlayer = false;
            agent.SetDestination(startPosition);
        }
    }
}
