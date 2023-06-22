using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void voltar_Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
