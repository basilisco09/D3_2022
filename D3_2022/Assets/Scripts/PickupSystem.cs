using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [HideInInspector] public ItemController itemController;
    [HideInInspector] public GunController gunController;
    private bool isItem = false;
    private bool isGun = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isItem)
            {
                PickupItem();
            }
            if(isGun)
            {
                PickupGun();
            }
        }
    }

    public void PickupItem()
    { 
        InventoryManager.Instance.AddItem(itemController.item);
        Destroy(itemController.gameObject);
    }

    public void PickupGun()
    {
        InventoryManager.Instance.AddGun(gunController.gun);
        Destroy(gunController.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Im here");
        if(collider.tag == "Item")
        {
            itemController = collider.gameObject.GetComponent<ItemController>();
            isItem = true;
            Debug.Log("Is touching a item");
        }
        if(collider.tag == "Gun")
        {
            gunController = collider.gameObject.GetComponent<GunController>();
            isGun = true;
            Debug.Log("Is touching a gun");
        }
    }
}
