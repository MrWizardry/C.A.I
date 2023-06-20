using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Interface : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Scenes/Scene_Pedro");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
