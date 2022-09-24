using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0) Die();
    }

    public void Healing(int cure)
    {
        currentHealth += cure;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
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
        audioSource.PlayOneShot(deathSound, 1f);
        Destroy(gameObject);
        Time.timeScale = 0f;
    }
}
