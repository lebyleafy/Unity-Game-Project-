using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public PauseMenuUI Menuoff;


    public void Setup()
    {
        gameObject.SetActive(true);

    }
    public void Stop()
    {
        gameObject.SetActive(false);
        Menuoff.MenuOff();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 2");
        Menuoff.MenuOff();
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
