using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public int meleeDamage;
    public LayerMask enemyLayers;
    public AudioSource audioSource;
    public AudioClip attackAudio;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); 

        //Damage then
        foreach (Collider2D enemy in hitEnemies)
        {               
            enemy.GetComponent<EnemyLifeSystem>().TakeDamage(meleeDamage);
            audioSource.PlayOneShot(attackAudio, 1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

