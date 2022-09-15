using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public Guns gun;
    public Transform firePoint;

    [HideInInspector]public int bulletsInMagazine;
    [HideInInspector]public bool hasBulletInMagazine;
    [HideInInspector]public int magazineSize;
    [HideInInspector]public int fireRate;
    [HideInInspector]public float reloadTime;

    [HideInInspector]public bool isReloading;
    [HideInInspector]public float cooldownTime;
    [HideInInspector]public float nextFireTime = 0;
    [HideInInspector]public float stoppingReload = 0;

    public PauseMenu pauseMenu;

    void Start ()
    {
        magazineSize = gun.magazineSize;
        cooldownTime = gun.cooldownTime;
        reloadTime = gun.reloadTime;
        bulletsInMagazine = magazineSize;
    }
    void Update()
    {
        if(bulletsInMagazine <= 0)
        {
            hasBulletInMagazine = false;
            Debug.Log("Has no ammo!");
        }
        else
        {
            hasBulletInMagazine = true;
        } 

        if(!pauseMenu.GameIsPaused)
        {
            if(Input.GetButtonDown("Fire1") && hasBulletInMagazine && Time.time > nextFireTime && !isReloading)
            {
                Shoot();
                bulletsInMagazine -= 1;
                nextFireTime = Time.time + cooldownTime;
            }
            
            if(Input.GetKeyDown(KeyCode.R) && bulletsInMagazine != magazineSize && Time.time > stoppingReload)
            {
                Reload();
                stoppingReload = Time.time + reloadTime;
            }
        }  
    }

    void Shoot ()
    {
        Instantiate(gun.bullet, firePoint.position, firePoint.rotation);
    }

    void Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        bulletsInMagazine = magazineSize;
        isReloading = false;
    }
}
