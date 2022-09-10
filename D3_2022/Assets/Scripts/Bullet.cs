using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsOnScreen;
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _direction = (_playerMovement.lookDirection).normalized;
        _rb.velocity =  _direction * bulletSpeed;

        Destroy(gameObject, secondsOnScreen);
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Destroy(gameObject);
    }

}
