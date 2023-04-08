using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretRoomScript : MonoBehaviour
{
    [SerializeField] int[] hpValue = new int[4] { 10, 15, 20, 25 };
    [SerializeField] int[] goldValue = new int[4] { 10, 15, 20, 25 };
    [SerializeField] int rand = 0;    
    [SerializeField] int eventRand = 0;    
    [SerializeField] GameObject secretButton;
    [SerializeField] TextMeshProUGUI secretButtonText;


    #region ������
    public void Heal()
    {
        GameManager.playerStatus.hp += hpValue[rand];
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        {
            GameManager.playerStatus.hp = GameManager.playerStatus.maxHp;
        }
    }

    public void BonusGold()
    {        
        GameManager.playerGold += goldValue[rand];
    }

    public void SpecialItem()
    {
        Debug.Log("SpecialItem");

    }
    #endregion

    #region ������
    public void Damaged()
    {     
        GameManager.playerStatus.hp -= hpValue[rand];
        if (GameManager.playerStatus.hp <= 0)
        {
            // ��� ó��
            Debug.Log("You Dead. Fool");            
        }
    }

    public void LoseGold()
    {
      
        GameManager.playerGold -= goldValue[rand];
    }

    public void StealItem()
    {
        Debug.Log("StealItem");

    }
    #endregion


    void Start()
    {
        eventRand = Random.Range(0, 6);

        switch (eventRand)
        {
            case 0:
                Debug.Log("Heal");
                rand = Random.Range(0, 4);
                secretButtonText.text = "You Can Get Heal!\n" + "��ġ: " + hpValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(Heal);
                break;
            case 1:
                Debug.Log("BonusGold");
                rand = Random.Range(0, 4);
                secretButtonText.text = "You Can Get Gold!\n" + "��ġ: " + goldValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(BonusGold);
                break;
            case 2:
                secretButton.GetComponent<Button>().onClick.AddListener(SpecialItem);
                break;
            case 3:
                Debug.Log("Damaged");
                rand = Random.Range(0, 4);
                secretButtonText.text = "You meet robbery!\n" + "��ġ: " + hpValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(Damaged);
                break;
            case 4:
                Debug.Log("LoseGold");
                rand = Random.Range(0, 4);
                secretButtonText.text = "you got your gold stolen!\n" + "��ġ: " + goldValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(LoseGold);
                break;
            case 5:
                secretButton.GetComponent<Button>().onClick.AddListener(StealItem);
                break;
            default:
                Debug.Log("��й� ����!");
                break;
        }

    }

    void Update()
    {
        
    }
}
