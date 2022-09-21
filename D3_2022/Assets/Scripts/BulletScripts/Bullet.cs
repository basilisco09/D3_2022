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
    public int _bulletDamage = 10;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _direction = (_playerMovement.lookDirection).normalized;
        _rb.velocity =  _direction * bulletSpeed; 

        Destroy(gameObject, secondsOnScreen);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {   
        if(hitInfo.tag == "Enemy")
        {
            hitInfo.GetComponent<EnemyLifeSystem>().TakeDamage(_bulletDamage);
        }
        if(hitInfo.tag != "River" && hitInfo.tag != "Gun") Destroy(gameObject);
    }

}
