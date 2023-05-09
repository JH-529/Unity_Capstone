using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCScript : MonoBehaviour
{
    public GameObject mainOptionCanvas;
    public GameObject battleOptionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(battleOptionCanvas)
        {
            battleOptionCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainOptionCanvas.activeSelf)
            {
                mainOptionCanvas.SetActive(true);
            }
            else
            {
                mainOptionCanvas.SetActive(false);
            }

            if(!battleOptionCanvas.activeSelf)
            {
                battleOptionCanvas.SetActive(true);
            }
            else
            {
                battleOptionCanvas.SetActive(false);
            }
        }
    }
}
