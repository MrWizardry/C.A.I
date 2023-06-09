using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy_Follow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    private Vector3 _startPos;
    private bool _hasReachedPlayer = false;
    private bool _canSeePlayer = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPos = transform.position;
    }

    private void Update()
    {
        if (_canSeePlayer)
        {
            //_agent.SetDestination(player.position);
            //_hasReachedPlayer = true;
            SceneManager.LoadScene("Derrota[]");
        }
        else if (_hasReachedPlayer)
        {
            _agent.SetDestination(_startPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSeePlayer = false;
        }
    }
}
