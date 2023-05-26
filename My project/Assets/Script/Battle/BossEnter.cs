using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;

    bool toBossChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BossSetting()
    {
        enemy.SetActive(false);
        boss.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!toBossChanged && GameManager.inboss)
        {
            Debug.Log("º¸½ºÀü");
            BossSetting();
            toBossChanged = true;
        }
    }
}
