using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Se cerró el Juego");
    }
}