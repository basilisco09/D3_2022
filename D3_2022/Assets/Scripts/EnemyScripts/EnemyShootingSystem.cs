using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingSystem : MonoBehaviour
{
    public GameObject gunGO;
    private Guns gun;
    private Transform firePoint;
    public LayerMask playerLayer;

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
    private Enemies enemy;
    private float attackRange;
    private Transform gunSpawn;

    void Start()
    {   gunSpawn = transform.Find("GunSpawn");
        Instantiate(gunGO, gunSpawn.position, gunSpawn.rotation, gunSpawn);
        enemy = GetComponent<EnemyController>().enemy;
        attackRange = enemy.attackRange;
    }

    void Update()
    {
        
        Collider2D player = Physics2D.OverlapCircle(this.transform.position, attackRange, playerLayer);
        if(player == null) return;
        GetGunAttributes();
            if(gun.isAutomatic)
            {
                if(hasBulletInMagazine && Time.time > nextFireTime &&!isReloading)
                {
                    Shoot();
                    bulletsInMagazine -= 1;
                    nextFireTime = Time.time + cooldownTime;
                } 
            }
            else
            {
                if(hasBulletInMagazine && Time.time > nextFireTime && !isReloading)
                {
                    Shoot();
                    bulletsInMagazine -= 1;
                    nextFireTime = Time.time + cooldownTime;
                }
            }
                
            if(bulletsInMagazine < magazineSize)
            {
                StartCoroutine(Reload());
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
