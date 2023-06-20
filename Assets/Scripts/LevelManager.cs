using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int totalObjectsToCollect; // The total number of objects to collect

    private int collectedObjectCount;

    public void CollectObject()
    {
        collectedObjectCount++;

        // Check if all objects have been collected
        if (collectedObjectCount >= totalObjectsToCollect)
        {
            ReturnToMenu();
        }
    }

    private void ReturnToMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Victory[]");
    }
}
