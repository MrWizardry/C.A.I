using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Follow : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatsisground, whatsisplayer;

    //patrol
    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkPointRange;

    //states
    public float SightRange;
    public bool playerInSight;
    private void Awake()
    {
        player = GameObject.Find("ThirdPesronPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, SightRange, whatsisplayer);

        if (!playerInSight) Patrolling();
        if (playerInSight) ChasePlayer();
    }

    private void Patrolling()
    {
        if (!walkpointSet) SearchWalkPoint();

        if (walkpointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkpointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatsisground))
        {
            walkpointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
