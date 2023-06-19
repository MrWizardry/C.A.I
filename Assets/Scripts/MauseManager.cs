using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseManager : MonoBehaviour
{
    private bool isCursorLocked = false;

    private void Start()
    {
        Cursor.visible = true; // Make sure the cursor is visible in the menu scene
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor in the menu scene
        isCursorLocked = false; // Set the cursor lock state to false
    }

    private void Update()
    {
        // Toggle cursor lock state when the player clicks anywhere in the menu scene
        if (Input.GetMouseButtonDown(0))
        {
            isCursorLocked = !isCursorLocked;
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }
    }
}
