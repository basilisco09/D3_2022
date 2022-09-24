using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    Hotbar hotbar;
    public static InventoryManager Instance;
    public List<Items> Items = new List<Items>();
    public List<Guns> Guns = new List<Guns>();
    
    

    public void Awake()
    {
        Instance = this;
    }

    public void AddItem(Items item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Items item)
    {
        Items.Remove(item);
    }

    public void AddGun(Guns gun)
    {
        Guns.Add(gun);
    }

    


    void Update()
    {
        
    }



}
