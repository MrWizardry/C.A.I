using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable_System : MonoBehaviour
{
    public Sprite collectedIcon; // The icon to display when the object is collected

    private bool isCollected;
    private Image collectedObjectIcon;

    private void Start()
    {
        collectedObjectIcon = ScoreManager.Instance.GetNextCollectedObjectIcon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Collect();
            GameObject.FindObjectOfType<LevelManager>().CollectObject(); // Call the CollectObject method in LevelManager
        }
    }

    private void Collect()
    {
        isCollected = true;

        // Update UI to show collected object icon
        collectedObjectIcon.sprite = collectedIcon;
        collectedObjectIcon.gameObject.SetActive(true);

        // Remove the collectible from the scene
        Destroy(gameObject);
    }
}
