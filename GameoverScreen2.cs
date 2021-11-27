using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScreen2 : MonoBehaviour
{
    public bool lockCursor = true;
    private bool m_cursorIsLocked = true;

    public void Setup()
    {
        gameObject.SetActive(true);
        UpdateCursorLock();
        
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }

        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
