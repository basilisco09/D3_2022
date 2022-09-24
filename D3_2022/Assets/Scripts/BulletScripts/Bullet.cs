using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsOnScreen;
    private Transform _shooterTransform;
    private Transform _gunSpawnTransform;
    private Rigidbody2D _rb;
    private Vector3 _direction;
    public int bulletDamage = 10;

    [HideInInspector]public GameObject bullet1;
    [HideInInspector]public GameObject bullet2;
    [HideInInspector]public GameObject bullet3;
    [HideInInspector]public Rigidbody2D rb1;
    [HideInInspector]public Rigidbody2D rb2;
    [HideInInspector]public Rigidbody2D rb3;

    void Start()
    {
        _gunSpawnTransform = this.transform.parent.GetComponentInParent<Transform>();
        _shooterTransform = _gunSpawnTransform.parent.transform;
        _rb = GetComponent<Rigidbody2D>();
        if(_rb != null)
        {
            _direction = _gunSpawnTransform.position - _shooterTransform.position;
            _rb.velocity =  _direction * bulletSpeed; 
        }
        else
        {
            bullet1 = GetComponent<Transform>().Find("Bullet1").gameObject;
            bullet2 = GetComponent<Transform>().Find("Bullet2").gameObject;
            bullet3 = GetComponent<Transform>().Find("Bullet3").gameObject;

            rb1 = bullet1.GetComponent<Rigidbody2D>();
            _direction = bullet1.transform.position - _shooterTransform.position;
            rb1.velocity =  _direction * bulletSpeed; 

            rb2 = bullet2.GetComponent<Rigidbody2D>();
            _direction = bullet2.transform.position - _shooterTransform.position;
            rb2.velocity =  _direction * bulletSpeed; 

            rb3 = bullet3.GetComponent<Rigidbody2D>();
            _direction = bullet3.transform.position - _shooterTransform.position;
            rb3.velocity =  _direction * bulletSpeed; 
        }

        Destroy(gameObject, secondsOnScreen);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {   
        if(hitInfo.tag == "Enemy")
        {
            hitInfo.GetComponent<EnemyLifeSystem>().TakeDamage(bulletDamage);
        }
        if(hitInfo.tag == "Player")
        {
            hitInfo.GetComponent<PlayerLifeSystem>().TakeDamage(bulletDamage);
        }
        if(hitInfo.tag != "River" && hitInfo.tag != "Gun" && hitInfo.tag != "Bullet") Destroy(gameObject);
    }

}
