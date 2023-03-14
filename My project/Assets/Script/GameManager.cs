using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UnitCode
{   
    empty,
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

    public Status() { unitcode = UnitCode.empty; }
    public Status(UnitCode code, float maxhp, float hp, float attack, float defence, int gold = 0)
    {
        this.unitcode = code;
        this.maxHp = maxhp;
        this.hp = hp;
        this.attack = attack;
        this.defence = defence;
        this.gold = gold;
    }
  
    // Object�� �ش� UnitCode�� Status �ο�
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
    public static Status playerStatus = new Status();
    public static Status enemyStatus = new Status();
    /*�÷��̾��� ī�� ���*/

    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;
    public TextMeshProUGUI UIhpBarText_Player;
    public TextMeshProUGUI UIhpBarText_Enemy;

    public Slider playerHpBar;
    public Slider ememyHpBar;

    // GameManager�� SingleTon����
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        if(playerStatus.unitcode == UnitCode.empty)
            playerStatus = playerStatus.SetUnitStatus(UnitCode.player);
        playerHpBar = GameObject.Find("Player_Hp_Slider").GetComponent<Slider>();       
        playerHpBar.value = playerStatus.hp / playerStatus.maxHp;

        if(enemyStatus.unitcode == UnitCode.empty)
           enemyStatus = enemyStatus.SetUnitStatus(UnitCode.enemy_normal);
        ememyHpBar = GameObject.Find("Enemy_Hp_Slider").GetComponent<Slider>();
        ememyHpBar.value = enemyStatus.hp / enemyStatus.maxHp;

        // ���̵� ���� â���� �̵��Ҷ� ���� NullReference ���� -> ���� ���� �ʿ�
        if (GameObject.Find("HPText").GetComponent<TextMeshProUGUI>())       
            UIhp = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();            
        if (GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>())
            UIGold = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>())
            UIhpBarText_Player = GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>())
            UIhpBarText_Enemy = GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIhp != null && UIGold != null)
        {
            UIhp.text = playerStatus.hp + " / " + playerStatus.maxHp;
            UIGold.text = playerStatus.gold + " G";
        }

        if(playerHpBar != null && ememyHpBar != null)
        {
            playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
            ememyHpBar.value = enemyStatus.hp / enemyStatus.maxHp;
        }

        if (UIhpBarText_Player != null && UIhpBarText_Enemy != null)
        {
            UIhpBarText_Player.text = playerStatus.hp + " / " + playerStatus.maxHp;
            UIhpBarText_Enemy.text = enemyStatus.hp + " / " + enemyStatus.maxHp;
        }
    }
}
