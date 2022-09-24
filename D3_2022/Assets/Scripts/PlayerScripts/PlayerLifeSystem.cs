using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) Die();
    }

    public void Healing(int cure)
    {
        currentHealth += cure;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void MaxHealthUpgrade(int upgrade, int cure)
    {
        maxHealth += upgrade;
        currentHealth += cure;
    }

    void Die()
    {
        Debug.Log("Player died");
        audioSource.PlayOneShot(deathSound, 1f);
        Destroy(gameObject);
        Time.timeScale = 0f;
    }
}
