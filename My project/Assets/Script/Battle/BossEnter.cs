using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public GameObject enemy;
    public GameObject interEnemy;
    public GameObject boss;

    bool toBossChanged = false;
    bool toInterChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BeginSetting()
    {
        enemy.SetActive(true);
        interEnemy.SetActive(false);
        boss.SetActive(false);
    }

    public void InterSetting()
    {
        enemy.SetActive(false);
        interEnemy.SetActive(true);
        boss.SetActive(false);
    }

    public void BossSetting()
    {
        enemy.SetActive(false);
        interEnemy.SetActive(false);
        boss.SetActive(true);
    }  

    // Update is called once per frame
    void Update()
    {
        if (!toBossChanged && GameManager.inboss)
        {
            Debug.Log("보스전");
            BossSetting();
            toBossChanged = true;
        }

        if (!toInterChanged && GameManager.inInter)
        {
            Debug.Log("중급");
            InterSetting();
            toInterChanged = true;
        }

        if(!GameManager.inInter && !GameManager.inboss)
        {
            BeginSetting();
            toInterChanged = false;
        }
    }
}
