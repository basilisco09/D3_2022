using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public Guns gun;
    public Transform firePoint;

    void Update()
    {
        Shoot();
    }

    void Shoot ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(gun.bullet, firePoint.position, firePoint.rotation);
        }
    }
}
