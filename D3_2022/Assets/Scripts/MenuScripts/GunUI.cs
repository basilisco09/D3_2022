using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    private ShootingSystem shootingSystem;
    private GameObject gunUI;
    private GameObject gunSlot;
    private GameObject totalAmmoDisplay;
    private GameObject ammoInMagazineDisplay;
    private Sprite gunSprite;
    private int totalAmmo;
    private int ammoInMagazine;

    void Start()
    {
        shootingSystem = FindObjectOfType<ShootingSystem>();
        gunUI = GameObject.Find("GunUI");
        totalAmmoDisplay = GameObject.Find("TotalAmmoDisplay");
        ammoInMagazineDisplay = GameObject.Find("CurrentAmmoDisplay");
        gunSlot = GameObject.Find("GunSlot");
    }

    void Update()
    {
        totalAmmo = shootingSystem.totalAmmo;
        ammoInMagazine = shootingSystem.bulletsInMagazine;
        gunSprite = shootingSystem.gun.gunSprite;
        if(gunSprite == null) gunSlot.GetComponent<Image>().enabled = false;
        else gunSlot.GetComponent<Image>().enabled = true;
        gunSlot.GetComponent<Image>().sprite = gunSprite;
        if(ammoInMagazine < shootingSystem.magazineSize * 0.3f) ammoInMagazineDisplay.GetComponent<Text>().color = Color.red;
        else ammoInMagazineDisplay.GetComponent<Text>().color = new Color(0.003921569f, 0.9333333f, 0.01568628f, 1f);;
        if(totalAmmo < 0) totalAmmo = 0;
        if(ammoInMagazine < 0) ammoInMagazine = 0;
        totalAmmoDisplay.GetComponent<Text>().text = totalAmmo.ToString("00");
        ammoInMagazineDisplay.GetComponent<Text>().text = ammoInMagazine.ToString("00");
    }
}
