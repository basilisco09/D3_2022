using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _player;
    private Transform _playerTransform;
    private Transform _enemyTransform;
    private Rigidbody2D _rb; 
    private Vector2 _movement;
    private Vector3 _direction;
    private float _moveSpeed;
    private float _detectionRadius;
    private bool playerIsClose;
    private bool playerInFOV;
    public LayerMask playerLayer;
    public float stopRadius = 1f;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _enemyTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _moveSpeed = GetComponent<EnemyController>().enemy.enemySpeed;
        _detectionRadius = GetComponent<EnemyController>().enemy.enemyFOV;
    }

    void Update()
    {
        //_movement = _direction;
        StopCircle();
        DetectionCircle();
    }

    void FixedUpdate()
    {
        if(playerInFOV)
        {
            if(!playerIsClose)
            {
                //_rb.MovePosition(_enemyTransform.position + (_direction * _moveSpeed * Time.deltaTime));
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _direction = _playerTransform.position - _enemyTransform.position;
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 78f;
                _rb.rotation = angle;
                _direction.Normalize();
            }
        }    
    }

    void StopCircle()
    {
        Collider2D player = Physics2D.OverlapCircle(this.transform.position, stopRadius, playerLayer);
        if(player == null)
        {
            playerIsClose = false;
            return;
        }
        else 
        {
            playerIsClose = true;
        }
    }

    void DetectionCircle()
    {
        Collider2D player = Physics2D.OverlapCircle(this.transform.position, _detectionRadius, playerLayer);
        if(player == null)
        {
            playerInFOV = false;
            return;
        }
        else 
        {
            playerInFOV = true;
        } 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, stopRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, _detectionRadius);
    }
}
