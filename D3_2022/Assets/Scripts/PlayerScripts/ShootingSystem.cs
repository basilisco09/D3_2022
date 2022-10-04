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
    [HideInInspector]public int totalAmmo;
    [HideInInspector]public bool hasTotalAmmo;
    [HideInInspector]public int fireRate;
    [HideInInspector]public float reloadTime;
    [HideInInspector]public bool hasGun = true;
    [HideInInspector]public bool isReloading = false;
    [HideInInspector]public bool hasChangedGun;
    [HideInInspector]public bool reloadCanceled = false;
    [HideInInspector]public float cooldownTime;
    [HideInInspector]public float nextFireTime = 0;
    [HideInInspector]public float stoppingReload = 0;
    [HideInInspector]public AudioSource audioSource;
    [HideInInspector]public AudioClip reloadAudio;
    [HideInInspector]public AudioClip shotAudio;
    public AudioClip noAmmoAudio;

    private PauseMenu pauseMenu;

    void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        pickupSystem = GetComponent<PickupSystem>();
        gunGO = GetComponent<PickupSystem>().weapon;
        firePoint = gunGO.transform.Find("FirePoint");
        audioSource = gunGO.GetComponent<AudioSource>();
        gun = gunGO.GetComponent<GunController>().gun;
        magazineSize = gun.magazineSize;
        cooldownTime = gun.cooldownTime;
        reloadTime = gun.reloadTime;
        reloadAudio = gun.reloadSound;
        shotAudio = gun.shotSound;
        bulletsInMagazine = magazineSize;
        totalAmmo = 5 * magazineSize;
        hasTotalAmmo = true;
    }

    void Update()
    {
        gunGO = GetComponent<PickupSystem>().weapon;
        if(pickupSystem.hasChangedGun) StartCoroutine(ReloadCancel());
        GetGunAttributes();
        if(pickupSystem.hasChangedGun)
        {
            Debug.Log("Mudou de arma");
            bulletsInMagazine = magazineSize;
            totalAmmo = 5 * magazineSize;
            hasTotalAmmo = true;
        }
        if(!pauseMenu.GameIsPaused)
        {
            if(gun.isAutomatic)
            {
                if(Input.GetMouseButton(0) && Time.time > nextFireTime)
                {
                    if(bulletsInMagazine > 0)
                    {
                        Shoot();
                        bulletsInMagazine -= 1;
                        nextFireTime = Time.time + cooldownTime;
                    }
                    else if(!hasBulletInMagazine && !hasTotalAmmo) if(!audioSource.isPlaying) audioSource.PlayOneShot(noAmmoAudio, 1f);
                } 
            }
            else
            {
                if(Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
                {
                    if(bulletsInMagazine > 0)
                    {
                        Shoot();
                        bulletsInMagazine -= 1;
                        nextFireTime = Time.time + cooldownTime;
                    }
                    else if(!hasBulletInMagazine && !hasTotalAmmo) if(!audioSource.isPlaying) audioSource.PlayOneShot(noAmmoAudio, 1f);
                }
            }
                
            if(bulletsInMagazine == 0 && hasTotalAmmo && !isReloading && !reloadCanceled)
            {
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot ()
    {   
        if(hasTotalAmmo)
        {
            Instantiate(gun.bullet, firePoint.position, firePoint.rotation, firePoint.transform);
            audioSource.PlayOneShot(shotAudio);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        if(totalAmmo > 0) audioSource.PlayOneShot(reloadAudio, 1f);
        yield return new WaitForSeconds(reloadTime);
        if(!reloadCanceled)
        {
            totalAmmo -= magazineSize;
            if(totalAmmo >= 0) 
            {
                bulletsInMagazine = magazineSize;
                hasTotalAmmo = true;
            }
            else
            {
                hasTotalAmmo = false;
            }
        }
        isReloading = false;
    }

    void GetGunAttributes()
    {
        firePoint = gunGO.transform.Find("FirePoint");
        audioSource = gunGO.GetComponent<AudioSource>();
        gun = gunGO.GetComponent<GunController>().gun;
        magazineSize = gun.magazineSize;
        cooldownTime = gun.cooldownTime;
        reloadTime = gun.reloadTime;
        reloadAudio = gun.reloadSound;
        shotAudio = gun.shotSound;

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

    IEnumerator ReloadCancel()
    {
        reloadCanceled = true;
        StopCoroutine(Reload());
        Debug.Log("Reload cancelado");
        yield return new WaitForSeconds(reloadTime);
        reloadCanceled = false;
    }
}
