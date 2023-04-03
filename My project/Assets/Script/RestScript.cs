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

    // Ư�� ������ŭ ü���� ȸ��
    public void GetRest()
    {
        GameManager.playerStatus.hp += (int)heal;
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }

        Debug.Log(healQuantity + "% ü�� ȸ��");
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    // �÷��̾��� �Ŀ��� 1 ���
    public void PlayerPowerUp()
    {
        GameManager.playerPower++;
        Debug.Log("���� �÷��̾� �Ŀ�: " + GameManager.playerPower);
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
        //Debug.Log("��ġ: " + healQuantity);
        ratio = 0.01f * (float)healQuantity;
        heal = GameManager.playerStatus.maxHp * ratio;
        
        restText.text = healQuantity.ToString() + "% ��ŭ ü�� ȸ�� (ȸ����: " + (int)heal + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
