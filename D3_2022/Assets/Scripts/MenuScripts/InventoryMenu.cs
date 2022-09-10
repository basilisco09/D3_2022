using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryMenuUI;
    public PauseMenu pauseMenu;
    [HideInInspector] public bool InventoryIsOpen = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!InventoryIsOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && InventoryIsOpen)
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        if(!pauseMenu.GameIsPaused)
        {
            inventoryMenuUI.SetActive(true);
            InventoryIsOpen = true;
        }
    }

    public void CloseInventory()
    {
        if(!pauseMenu.GameIsPaused && InventoryIsOpen)
        {
            inventoryMenuUI.SetActive(false);
            InventoryIsOpen = false;
        }
    }
}
