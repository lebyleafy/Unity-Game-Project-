using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameoverScreen GameOverScreen;


    public int maxHealth = 100;
    public int currentHealth;

    public Health healthBar;


    private WaitForSeconds regenTick = new WaitForSeconds(.2f);
    private Coroutine regen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage >= 0)
        {
            healthBar.slider.value = currentHealth;
            currentHealth -= damage;
            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenHealth());
        }
        if (currentHealth == 0)
        {
            GameOver();
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            TakeDamage(10);

        }

    }
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3);

        while (currentHealth < maxHealth)
        {
            currentHealth += maxHealth / 100;
            healthBar.slider.value = currentHealth;
            yield return regenTick;
        }
        regen = null;
    }
    public void GameOver()
    {
        GameOverScreen.Setup();
        Cursor.lockState = CursorLockMode.None;
    }
}