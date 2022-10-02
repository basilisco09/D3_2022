using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    private AudioSource audioSource;
    public GameObject retryMenuUI;
    public Timer timer;
    public bool playerIsDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        //retryMenuUI = GameObject.Find("Retry");
        healthBar.SetMaxHealth(maxHealth);
        audioSource = Camera.main.GetComponent<AudioSource>();
        timer = FindObjectOfType<Timer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(hurtSound, 1f);
            if (currentHealth <= 0)
            {
                Die();
            }
    }

    public void Healing(int cure)
    {
        currentHealth += cure;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

     public void MaxHealthUpgrade(int upgrade, int cure)
     {
        maxHealth += upgrade;
        currentHealth += cure;
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
     }

     void Die()
     {
        Debug.Log("Player died");
        playerIsDead = true;
        audioSource.PlayOneShot(deathSound, 1f);
        Destroy(gameObject);
        Time.timeScale = 0f;
        retryMenuUI.SetActive(true);
        Destroy(GameObject.Find("Musica(Clone)"));
        Destroy(GameObject.Find("Intro(Clone)"));
     }
}

