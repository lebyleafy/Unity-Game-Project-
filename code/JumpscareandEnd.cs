using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpscareandEnd : MonoBehaviour
{
    public GameoverScreen2 GameOverScreen;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            StartCoroutine(ExecuteAfterTime(3));           
        }

    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        GameOverScreen.Setup();
        Time.timeScale = 0;
    }

}
