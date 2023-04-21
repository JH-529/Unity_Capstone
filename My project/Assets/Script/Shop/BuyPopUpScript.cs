using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPopUpScript : MonoBehaviour
{
    [SerializeField] GameObject popUp;


    public void PopUpOff()
    {
        popUp.SetActive(false);
    }
}
