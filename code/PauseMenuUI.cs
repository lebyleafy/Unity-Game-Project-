using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    private Toggle m_MenuToggle;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;

    public PauseButton Pause;

    void Awake()
    {
        m_MenuToggle = GetComponent<Toggle>();
    }


    private void MenuOn()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        Pause.Setup();
    }


    public void MenuOff()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 1f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 1f;

    }



#if !MOBILE_INPUT
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            MenuOn();
        }

    }
#endif

}

