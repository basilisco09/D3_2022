using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public GameObject gunGO;
    public Guns gun;
    public Transform firePoint;
    public PickupSystem pickupSystem;

    [HideInInspector]public int bulletsInMagazine;
    [HideInInspector]public bool hasBulletInMagazine;
    [HideInInspector]public int magazineSize;
    [HideInInspector]public int fireRate;
    [HideInInspector]public float reloadTime;
    [HideInInspector]public bool hasGun = false;
    [HideInInspector]public bool isReloading = false;
    [HideInInspector]public bool hasChangedGun;
    [HideInInspector]public float cooldownTime;
    [HideInInspector]public float nextFireTime = 0;
    [HideInInspector]public float stoppingReload = 0;

    public PauseMenu pauseMenu;


    void Update()
    {
        gunGO = GetComponent<PickupSystem>().weapon;
        if(gunGO != null) hasGun = true;
        if(hasGun) GetGunAttributes();
        if(GetComponent<PickupSystem>().hasChangedGun == true) bulletsInMagazine = magazineSize;
        if(!pauseMenu.GameIsPaused)
        {
            if(hasGun)
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
                    
                if(Input.GetKeyDown(KeyCode.R) && bulletsInMagazine != magazineSize)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    void Shoot ()
    {
        Instantiate(gun.bullet, firePoint.position, firePoint.rotation);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        bulletsInMagazine = magazineSize;
        isReloading = false;
    }

    void GetGunAttributes()
    {
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
    }
}