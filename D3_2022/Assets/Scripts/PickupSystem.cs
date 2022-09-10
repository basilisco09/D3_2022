using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public Items item;

    public void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        Pickup();
    }
}
