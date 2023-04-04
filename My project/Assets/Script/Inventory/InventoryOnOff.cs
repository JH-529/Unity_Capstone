using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryOnOff : MonoBehaviour
{
    public GameObject inventory;

    public void InventoryControl()
    {
        if(!inventory.activeSelf)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false); 
        }
    }
}
