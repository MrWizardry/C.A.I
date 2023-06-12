using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_System : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    public GameObject collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collect();
    }

    private void Collect()
    {
        ScoreManager.Instance.IncreaseScore(scoreValue);

        if (collectEffect != null)
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
