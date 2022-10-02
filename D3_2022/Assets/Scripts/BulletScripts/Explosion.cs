using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int exDamage = 40;

    void Awake()
    {

        Destroy(this.gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Enemy")
        {
            hitInfo.GetComponent<EnemyLifeSystem>().TakeDamage(exDamage);
        }
        if (hitInfo.tag == "Player")
        {
            hitInfo.GetComponent<PlayerLifeSystem>().TakeDamage(exDamage);
        }
    }

  
}  


