using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretButtonScript : MonoBehaviour
{
    [SerializeField] Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.getKey)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }
}
