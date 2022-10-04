using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickupSystem : MonoBehaviour
{
    [HideInInspector] public GameObject item;
    [HideInInspector] public GameObject gun;
    [HideInInspector] public GameObject weapon;
    [HideInInspector] public ItemController itemController;
    [HideInInspector] public GunController gunController;
    [HideInInspector] public bool hasChangedGun = false;
    [HideInInspector] public PlayerLifeSystem playerLifeSystem;
    [HideInInspector] public PlayerMovement playerMovement;
    public GameObject startGun;
    public List<Image> icones;
    public LayerMask itemsLayer;
    public Transform gunSpawnTransform;
    public float circleRadius;
    private bool isItem = false;
    private bool isGun = false;
    public GameObject MessagePanel;
    public Hotbar hotbar;
    int i;
    public int slt = 0;
    public Sprite nada;

    void Awake()
    {
        playerLifeSystem = GetComponent<PlayerLifeSystem>();
        playerMovement = GetComponent<PlayerMovement>();
        gunSpawnTransform = transform.Find("SpawnGunPoint");
        weapon = Instantiate(startGun, gunSpawnTransform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.layer = 12;
        weapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<GunController>().gun.gunInGame;
        weapon.transform.Find("Sprite").localScale = new Vector3(1f, 1f, 1f);
        Destroy(weapon.transform.Find("Background").gameObject);
    }

    public void Update()
    {
        gunSpawnTransform = transform.Find("SpawnGunPoint");
        hasChangedGun = false;

        MakeACircle();
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Apertou E");
            if(isItem)
            {
                UseItem();
            }
            if(isGun)
            {
                hasChangedGun = true;
                PickupGun();
                isGun = false;
            }
        }
    }

    public void PickupGun()
    {
        InventoryManager.Instance.AddGun(gunController.gun);
        Debug.Log("Pegou a " + gun.name);
        Destroy(weapon);
        weapon = Instantiate(gun, gunSpawnTransform);
        Destroy(gun);
        weapon.transform.localPosition = Vector3.zero;
        weapon.layer = 12;
        weapon.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = gunController.gun.gunInGame;
        if(gunController.gun.gunName == "Pistol" || gunController.gun.gunName == "SMG") weapon.transform.Find("Sprite").localScale = new Vector3(1f, 1f, 1f);
        else if(gunController.gun.gunName == "Shotgun") weapon.transform.Find("Sprite").localScale = new Vector3(0.5f, 0.5f, 0.5f);
            else weapon.transform.Find("Sprite").localScale = new Vector3(4f, 4f, 4f);
        Destroy(weapon.transform.Find("Background").gameObject);
    }

    void MakeACircle()
    {
        Collider2D interactable = Physics2D.OverlapCircle(transform.position, circleRadius, itemsLayer);
        if (interactable == null)
        {
            isItem = false;
            isGun = false;
            return;
        }
        if (interactable.tag == "ItemHealer" || interactable.tag == "ItemBooster" || interactable.tag == "ItemHealthUpgrade")
        {
            isItem = true;
            isGun = false;
            item = interactable.gameObject;
            itemController = interactable.gameObject.GetComponent<ItemController>();
            Debug.Log("Is touching a item");
        }
        else
        { 
            if(interactable.tag == "Gun")
            {
                isGun = true;
                isItem = false;
                gun = interactable.gameObject;
                gunController = interactable.gameObject.GetComponent<GunController>();
                Debug.Log("Is touching a gun");
            }
            else
            {
                isItem = false;
                isGun = false;
            }
        }   
    }

    void UseItem()
    {
        if(item.tag == "ItemHealer")
        {
            int cure = itemController.item.cureValue;
            playerLifeSystem.Healing(cure);
            Debug.Log("Has cured " + cure);
        }

        if(item.tag == "ItemBooster")
        {
            float boost = itemController.item.moveSpeedBoost;
            float duration = itemController.item.boostDuration;
            StartCoroutine(playerMovement.MoveBoost(boost, duration));
        }

        if(item.tag == "ItemHealthUpgrade")
        {
            int maxHealthUpgrade = itemController.item.maxHealthUpgrade;
            int cure = itemController.item.cureValue;
            playerLifeSystem.MaxHealthUpgrade(maxHealthUpgrade, cure);
        }

        Destroy(item);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel(string text)
    {
        MessagePanel.SetActive(false);
    }
}
