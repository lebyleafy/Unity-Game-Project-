using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource collectSound;
    public GameoverScreen2 GameOver;

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectSound.Play();
            ScoringSystem.theScore += 1;
            Destroy(gameObject);
            if (ScoringSystem.theScore == 26)
            {
                GameOver.Setup();
                Time.timeScale = 0;

            }
        }
    }

}
