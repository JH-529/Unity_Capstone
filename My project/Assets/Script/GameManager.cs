using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UnitCode
{
    player,
    enemy_easy,
    enemy_normal,
    enemy_hard,
}

public class Status
{
    public UnitCode unitcode;
    public float maxHp;
    public float hp;
    public float attack;
    public float defence;
    public int gold;

    public Status() { }
    public Status(UnitCode code, float maxhp, float hp, float attack, float defence, int gold = 0)
    {
        this.unitcode = code;
        this.maxHp = maxhp;
        this.hp = hp;
        this.attack = attack;
        this.defence = defence;
        this.gold = gold;
    }
  
    // Object의 해당 UnitCode별 Status 부여
    public Status SetUnitStatus(UnitCode unitcode)
    {
        Status status = null;

        switch(unitcode)
        {
            case UnitCode.player:
                status = new Status(unitcode, 50, 50, 10, 5, 10);
                break;                      
            case UnitCode.enemy_easy:       
                status = new Status(unitcode, 30, 30, 5, 2);
                break;                      
            case UnitCode.enemy_normal:     
                status = new Status(unitcode, 50, 50, 7, 3);
                break;
            case UnitCode.enemy_hard:
                status = new Status(unitcode, 70, 70, 10, 4);
                break;
        }

        return status;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static Status playerStatus;
    /*플레이어의 카드 목록*/

    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;

    public Slider playerHpBar;
    public Slider ememyHpBar;

    // GameManager의 SingleTon패턴
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!gameManager)
            {
                gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (gameManager == null)
                    Debug.Log("no Singleton obj");
            }
            return gameManager;
        }
    }

    void Awake()
    {  
       
                
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = new Status();
        playerStatus = playerStatus.SetUnitStatus(UnitCode.player);
        playerHpBar = GameObject.Find("Player_Hp_Slider").GetComponent<Slider>();
        ememyHpBar = GameObject.Find("Enemy_Hp_Slider").GetComponent<Slider>();
        playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
        
        // 난이도 선택 창으로 이동할때 마다 NullReference 오류 -> 로직 변경 필요
        if (GameObject.Find("HPText").GetComponent<TextMeshProUGUI>())       
            UIhp = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();            
        if (GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>())
            UIGold = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (UIhp != null && UIGold != null)
        {
            UIhp.text = playerStatus.hp + " / " + playerStatus.maxHp;
            UIGold.text = playerStatus.gold + " G";
        }

        playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
    }
}
