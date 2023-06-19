using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable_System : MonoBehaviour
{
    [SerializeField] private Sprite collectedIcon;
    private bool isCollected;
    private Image collectedImage;

    private void Start()
    {
        collectedImage = ScoreManager.Instance.CollectedImage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Collect();
        }
    }

    private void Collect()
    {
        isCollected = true;

        collectedImage.sprite = collectedIcon;
        collectedImage.gameObject.SetActive(true);
        
        Destroy(gameObject);
    }
}
