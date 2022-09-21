using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public int meleeDamage;
    public LayerMask enemyLayers;

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
        {   if(enemy.tag == "Enemy")
            {
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<EnemyLifeSystem>().TakeDamage(meleeDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

