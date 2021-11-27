using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScreen : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
