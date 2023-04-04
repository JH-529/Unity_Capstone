using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestScript : MonoBehaviour
{
    int healQuantity = 0;
    float ratio = 0f;
    float heal = 0f;

    // 특정 비율만큼 체력을 회복
    public void GetRest()
    {
        GameManager.playerStatus.hp += (int)heal;
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }

        Debug.Log(healQuantity + "% 체력 회복");
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    // 플레이어의 파워를 1 상승
    public void PlayerPowerUp()
    {
        GameManager.playerPower++;
        Debug.Log("현재 플레이어 파워: " + GameManager.playerPower);
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject restObject = GameObject.Find("RestOption1").transform.GetChild(0).gameObject;
        GameObject restPower = GameObject.Find("RestOption2");
        restPower.GetComponent<Button>().onClick.AddListener(PlayerPowerUp);
        TextMeshProUGUI restText = restObject.GetComponent<TextMeshProUGUI>();

        healQuantity = Random.Range(20, 35);
        //Debug.Log("수치: " + healQuantity);
        ratio = 0.01f * (float)healQuantity;
        heal = GameManager.playerStatus.maxHp * ratio;
        
        restText.text = healQuantity.ToString() + "% 만큼 체력 회복 (회복량: " + (int)heal + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
