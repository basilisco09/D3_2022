using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    int i;
    int slt = 1;
    public PickupSystem pick;
    public InventoryManager invMan;
    public ItemController itemController;
    

    void Start()
    {
        
    }

    void UseItem(int slt)
    {
        isFull[slt] = false;
        Destroy(gameObject);



    }
    void DropItem(int slt)
    {
        isFull[slt] = false;
        

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slt = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slt = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slt = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slt = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            slt = 5 ;
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
}
