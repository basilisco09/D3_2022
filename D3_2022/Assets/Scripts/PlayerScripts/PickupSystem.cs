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
    

    public void Update()
    {
        gunSpawnTransform = transform.Find("SpawnGunPoint");
        hasChangedGun = false;

        void Use()
        {


        }


        MakeACircle();
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Apertou E");
            if(isItem)
            {
                /*for (int i = 0; i < 5; i++)
                {
                    if (hotbar.isFull[i] == false)
                    {
                        icones[i].sprite = item.GetComponent<SpriteRenderer>().sprite;
                        PickupItem();
                        hotbar.isFull[i] = true;
                        break;
                    }
                    
                }
                isItem = false;*/
                Use();

            }
            if(isGun)
            {
                hasChangedGun = true;
                PickupGun();
                isGun = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slt = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slt = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slt = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slt = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            slt = 4;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Apertou Space");
            UseItem(slt);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Apertou Q");
            DropItem(slt);
        }

    }
    void UseItem(int slt)
    {
        InventoryManager.Instance.RemoveItem(itemController.item);
        hotbar.isFull[slt] = false;
        icones[slt].sprite = nada;

    }
    void DropItem(int slt)
    {
        InventoryManager.Instance.RemoveItem(itemController.item);
        hotbar.isFull[slt] = false;
        icones[slt].sprite = nada;
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
        Destroy(gun);
        weapon = Instantiate(gun, gunSpawnTransform);
        weapon.transform.localPosition = Vector3.zero;
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
