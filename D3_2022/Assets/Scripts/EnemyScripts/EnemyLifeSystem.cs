using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeSystem : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    public Contador contaMortes;
    public bool canCount = true;

    void Start()
    {
        maxHealth = GetComponent<EnemyController>().enemy.enemyHealth;
        currentHealth = maxHealth;
        contaMortes = FindObjectOfType<Contador>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Debug.Log("Enemy died");
        Destroy(gameObject);
        contaMortes.ContaMortes(1);
        canCount = false;
    }
}
