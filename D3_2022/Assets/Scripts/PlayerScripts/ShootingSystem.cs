using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public GameObject gunGO;
    [HideInInspector] Guns gun;
    public Transform firePoint;
    public PickupSystem pickupSystem;

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

    void Start()
    {
        bulletsInMagazine = magazineSize;
    }

    void Update()
    {
        gunGO = GetComponent<PickupSystem>().weapon;
        firePoint = gunGO.transform.Find("FirePoint");
        gun = gunGO.GetComponent<GunController>().gun;
        magazineSize = gun.magazineSize;
        cooldownTime = gun.cooldownTime;
        reloadTime = gun.reloadTime;

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
            if(gun.isAutomatic)
            {
               if(Input.GetMouseButton(0) && hasBulletInMagazine && Time.time > nextFireTime &&!isReloading)
                {
                    Shoot();
                    bulletsInMagazine -= 1;
                    nextFireTime = Time.time + cooldownTime;
                } 
            }
            else
            {
                if(Input.GetMouseButtonDown(0) && hasBulletInMagazine && Time.time > nextFireTime && !isReloading)
                {
                    Shoot();
                    bulletsInMagazine -= 1;
                    nextFireTime = Time.time + cooldownTime;
                }
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
