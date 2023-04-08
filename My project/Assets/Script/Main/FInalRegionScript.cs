using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalRegionScript : MonoBehaviour
{
    [SerializeField] GameObject BGimage;

    // Start is called before the first frame update
    void Start()
    {
        BGimage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.playerLevel >= 5)
        {
            BGimage.SetActive(false);
        }
    }
}
