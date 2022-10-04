using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingSystem : MonoBehaviour
{
    public GameObject gunGO;
    private GameObject weapon;
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
    [HideInInspector]public AudioSource audioSource;
    [HideInInspector]public AudioClip reloadAudio;
    [HideInInspector]public AudioClip shotAudio;
    private Enemies enemy;
    private float attackRange;
    private Transform gunSpawn;
    private EnemyMovement enemyMovement;

    void Start()
    {   
        enemyMovement = GetComponent<EnemyMovement>();
        gunSpawn = transform.Find("GunSpawn");
        weapon = Instantiate(gunGO, gunSpawn);
        weapon.transform.localPosition = Vector3.zero;
        Destroy(weapon.transform.Find("Background").gameObject);
        weapon.GetComponentInChildren<SpriteRenderer>().sprite = null;
        weapon.layer = 12;
        if(gun.gunName == "Pistol" || gun.gunName == "SMG") weapon.transform.Find("Sprite").localScale = new Vector3(1f, 1f, 1f);
        else if(gun.gunName == "Shotgun") weapon.transform.Find("Sprite").localScale = new Vector3(0.5f, 0.5f, 0.5f);
            else weapon.transform.Find("Sprite").localScale = new Vector3(4f, 4f, 4f);
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
                
            if(bulletsInMagazine == 0)
            {
                StartCoroutine(Reload());
            }
    }

    void Shoot ()
    {
        Instantiate(gun.bullet, firePoint.position, firePoint.rotation, gunSpawn);
        audioSource.PlayOneShot(shotAudio);
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
        firePoint = weapon.transform.Find("FirePoint");
        audioSource = weapon.GetComponent<AudioSource>();
        gun = weapon.GetComponent<GunController>().gun;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
