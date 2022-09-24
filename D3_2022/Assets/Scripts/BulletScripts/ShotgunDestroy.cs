using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunDestroy : MonoBehaviour
{
    private float secondsOnScreen;
    private int bulletDamage;

    void Start()
    {
        secondsOnScreen = GetComponentInParent<Bullet>().secondsOnScreen;
        bulletDamage = GetComponentInParent<Bullet>().bulletDamage;
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
