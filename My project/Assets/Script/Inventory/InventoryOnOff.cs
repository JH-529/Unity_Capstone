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
            Debug.Log("살려라");
            inventory.SetActive(true);
        }
        else
        {
            Debug.Log("죽여라");
            inventory.SetActive(false); 
        }
    }

}
