using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [HideInInspector] public GameObject item;
    [HideInInspector] public GameObject gun;
    [HideInInspector] public ItemController itemController;
    [HideInInspector] public GunController gunController;
    private bool isItem = false;
    private bool isGun = false;
    public GameObject MessagePanel;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
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
        Destroy(item);
    }

    public void PickupGun()
    {
        InventoryManager.Instance.AddGun(gunController.gun);
        Destroy(gun);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Im here");
        if (collider.tag == "Item")
        {
            isItem = true;
            item = collider.gameObject;
            itemController = collider.gameObject.GetComponent<ItemController>();
            isGun = false;
            Debug.Log("Is touching a item");
        }
        else
        { 
            if(collider.tag == "Gun")
            {
                isGun = true;
                isItem = false;
                gun = collider.gameObject;
                gunController = collider.gameObject.GetComponent<GunController>();
                Debug.Log("Is touching a gun");
            }
            else
            {
                isItem = false;
                isGun = isItem;
            }
        }  
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
