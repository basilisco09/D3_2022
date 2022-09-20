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

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _enemyTransform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _moveSpeed = GetComponent<EnemyController>().enemy.enemySpeed;
    }

    void Update()
    {
        _direction = _playerTransform.position - _enemyTransform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
        _direction.Normalize();
        _movement = _direction;
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_enemyTransform.position + (_direction * _moveSpeed * Time.deltaTime));
    }

}
