using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StopSoundOnPlayerDead : MonoBehaviour
{
    public AudioSource m_MyAudioSource;
    public GameObject videoPlayer;

    void Start()
    {
        m_MyAudioSource.Play();
        videoPlayer.SetActive(false);      
        
    }


    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag == "Player")
        {

            videoPlayer.SetActive(true);
            Destroy(videoPlayer, 3);
            m_MyAudioSource.Stop();

        }
    }
}
