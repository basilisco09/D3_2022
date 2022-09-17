using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [HideInInspector] public GameObject item;
    [HideInInspector] public GameObject gun;
    [HideInInspector] public GameObject weapon;
    [HideInInspector] public ItemController itemController;
    [HideInInspector] public GunController gunController;
    public LayerMask itemsLayer;
    public Transform gunSpawnTransform;
    public float circleRadius;
    private bool isItem = false;
    private bool isGun = false;
    public GameObject MessagePanel;

    public void Update()
    {
        gunSpawnTransform = transform.Find("SpawnGunPoint");

        MakeACircle();
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Apertou E");
            if(isItem)
            {
                PickupItem();
                isItem = false;
            }
            if(isGun)
            {
                PickupGun();
                isGun = false;
            }
        }
    }

    public void PickupItem()
    { 
        InventoryManager.Instance.AddItem(itemController.item);
        Debug.Log("Pegou um " + item.name);
        Destroy(item);
    }

    public void PickupGun()
    {
        InventoryManager.Instance.AddGun(gunController.gun);
        Debug.Log("Pegou a " + gun.name);
        Destroy(weapon);
        weapon = Instantiate(gun, gunSpawnTransform);
        weapon.transform.localPosition = Vector3.zero;
        Destroy(gun);
        
    }

    void MakeACircle()
    {
        Collider2D interactable = Physics2D.OverlapCircle(transform.position, circleRadius, itemsLayer);
        if (interactable == null) return;
        if (interactable.tag == "Item")
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
