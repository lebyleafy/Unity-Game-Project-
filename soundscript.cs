using UnityEngine;


public class soundscript : MonoBehaviour
{

    public AudioSource audioObject; // drag the music player here or...


    void OnTriggerEnter(Collider other)
    {
        audioObject.Play();
    }

}