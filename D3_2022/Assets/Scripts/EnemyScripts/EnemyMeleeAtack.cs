using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAtack : MonoBehaviour
{
    private Enemies _enemy;
    private float _nextAttack = 0;
    private float _attackRange;
    private Transform _attackPoint;
    private int _enemyDamage;
    public LayerMask playerLayer;

    void Start()
    {
        _attackPoint = transform.Find("AttackPoint");
        _enemy = GetComponent<EnemyController>().enemy;
        _attackRange = _enemy.attackRange;
        _enemyDamage = _enemy.enemyDamage; 
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, playerLayer);
        if(player == null) return;
        if(Time.time > _nextAttack)
        {
            Debug.Log("Acertou o player");
            player.GetComponent<PlayerLifeSystem>().TakeDamage(_enemyDamage);
            _nextAttack = Time.time + _enemy.attackCooldown;
        } 
    }

    void OnDrawGizmos()
    {
        if(_attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
