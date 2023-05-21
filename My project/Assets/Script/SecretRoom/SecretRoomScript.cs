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

    [Header("메인 인벤토리")]
    public Inventory mainInventory;
    [Header("배틀 인벤토리")]
    public Inventory battleInventory;
    public Item specialArmor;

    #region 좋은일
    public void Heal()
    {
       // Debug.Log("Get Heal");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Right);
        GameManager.playerStatus.hp += hpValue[rand];
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        {
            GameManager.playerStatus.hp = GameManager.playerStatus.maxHp;
        }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void BonusGold()
    {
        //Debug.Log("Get BonusGold");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Right);
        GameManager.playerGold += goldValue[rand];
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void SpecialItem()
    {
        //Debug.Log("Get SpecialItem");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.GetSpecialItem);
        mainInventory.AddItem(specialArmor);
        battleInventory.AddItem(specialArmor);
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion

    #region 나쁜일
    public void Damaged()
    {
        //Debug.Log("Get Damaged");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Wrong);
        GameManager.playerStatus.hp -= hpValue[rand];
        if (GameManager.playerStatus.hp <= 0)
        {
            // 사망 처리
            Debug.Log("You Dead. Fool");            
        }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void LoseGold()
    {
        //Debug.Log("Lose Gold");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Wrong);
        GameManager.playerGold -= goldValue[rand];
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void StealItem()
    {
        //Debug.Log("Get Item Stolen");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Wrong);
        if (mainInventory != null)
        {
            mainInventory.DeleteRandomItem();
            battleInventory.DeleteRandomItem();
        }

        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion


    void Start()
    {
        eventRand = Random.Range(0, 6);
        mainInventory.DeleteItem("SecretKey");
        battleInventory.DeleteItem("SecretKey");

        switch (eventRand)
        {
            case 0:
                //Debug.Log("Heal");
                rand = Random.Range(0, 4);
                secretButtonText.text = "체력을 회복하자!\n" + "수치: " + hpValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(Heal);
                break;
            case 1:
                //Debug.Log("BonusGold");
                rand = Random.Range(0, 4);
                secretButtonText.text = "골드를 얻었다!\n" + "수치: " + goldValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(BonusGold);
                break;
            case 2:
                secretButtonText.text = "특별한 아이템이...!\n";
                secretButton.GetComponent<Button>().onClick.AddListener(SpecialItem);
                break;
            case 3:
                //Debug.Log("Damaged");
                rand = Random.Range(0, 4);
                secretButtonText.text = "으악! 강도다!\n" + "수치: " + hpValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(Damaged);
                break;
            case 4:
                //Debug.Log("LoseGold");
                rand = Random.Range(0, 4);
                secretButtonText.text = "골드를 빼앗겼어!!!\n" + "수치: " + goldValue[rand].ToString();
                secretButton.GetComponent<Button>().onClick.AddListener(LoseGold);
                break;
            case 5:
                secretButtonText.text = "아니 내 아이템이!!!\n";
                secretButton.GetComponent<Button>().onClick.AddListener(StealItem);
                break;
            default:
                //Debug.Log("비밀방 오류!");
                break;
        }

    }

    void Update()
    {
        
    }
}
