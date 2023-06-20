using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public GridLayoutGroup collectedObjectsGrid; // Reference to the GridLayoutGroup UI element

    public List<Sprite> collectedObjectIcons = new List<Sprite>(); // List of collected object icons

    private int collectedObjectCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Image GetNextCollectedObjectIcon()
    {
        // Instantiate a new Image component for the next collected object
        GameObject newIconObject = new GameObject("CollectedObjectIcon");
        newIconObject.transform.SetParent(collectedObjectsGrid.transform);

        Image newIcon = newIconObject.AddComponent<Image>();
        newIcon.gameObject.SetActive(false); // Initially hide the icon

        // Set the icon sprite from the collectedObjectIcons list
        if (collectedObjectCount < collectedObjectIcons.Count)
        {
            newIcon.sprite = collectedObjectIcons[collectedObjectCount];
        }

        collectedObjectCount++;

        return newIcon;
    }
}
