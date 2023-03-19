using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DIFFICULTY
{ 
    NONE,
    EASY,
    NORMAL,
    HARD
}
public enum UNIT_TYPE
{   
    EMPTY,
    PLAYER,
    enemy_easy,
    enemy_normal,
    enemy_hard,
}
public enum CAMERA_TYPE
{
    MAIN,
    BATTLE,
    SHOP,
}

public class Status
{
    public UNIT_TYPE unitcode;
    public float maxHp;
    public float hp;    
    public float defence;

    public Status() { unitcode = UNIT_TYPE.EMPTY; }
    public Status(UNIT_TYPE code, float maxhp, float hp, float defence)
    {
        this.unitcode = code;
        this.maxHp = maxhp;
        this.hp = hp;
        this.defence = defence;
    }
  
    // Object의 UnitCode에 따라 정해진 Status를 반환
    public Status SetUnitStatus(UNIT_TYPE unitcode)
    {
        Status status = null;

        switch(unitcode)
        {
            case UNIT_TYPE.PLAYER:
                status = new Status(unitcode, 50, 50, 5);
                break;
            case UNIT_TYPE.enemy_easy:       
                status = new Status(unitcode, 30, 30, 5);
                break;
            case UNIT_TYPE.enemy_normal:     
                status = new Status(unitcode, 50, 50, 7);
                break;
            case UNIT_TYPE.enemy_hard:
                status = new Status(unitcode, 70, 70, 10);
                break;
        }

        return status;
    }
}
public class TextUI
{
    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;
    public TextMeshProUGUI UIhpBarText_Player;
    public TextMeshProUGUI UIhpBarText_Enemy;
    public Slider playerHpBar;
    public Slider ememyHpBar;
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static Status playerStatus = new Status();
    public static Status enemyStatus = new Status();
    public static DIFFICULTY difficulty = DIFFICULTY.NONE;
    public static CAMERA_TYPE cameraSelect;
    public static int playerGold = 0;

    public CameraManager cameraManager;

    public TextUI mainUI = new TextUI();
    public TextUI battleUI = new TextUI();
    public TextUI shopUI = new TextUI();
    public GameObject mainCanvas;
    public GameObject mainMapCanvas;
    public GameObject battleCanvas;
    public GameObject shopCanvas;

    public CardSO cardSO;
    public List<NumberCard> numberCards;
    public List<OperatorCard> operatorCards;

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

    public static void DifficultySetEasy() { difficulty = DIFFICULTY.EASY; }
    public static void DifficultySetNormal() { difficulty = DIFFICULTY.NORMAL; }
    public static void DifficultySetHard() { difficulty = DIFFICULTY.HARD; }

    void MakeEasyCardSet()
    {
        for(int i=0; i<10; i++)
        {
            NumberCard card = new NumberCard("name", i, null);
            numberCards.Add(card);
        }

        for(int i=0; i<4; i++)
        {
            OperatorCard card = new OperatorCard("operator", OperatorCard.OPERATOR_TYPE.PLUS, null);
            operatorCards.Add(card);
        }
    }
    
    void LoadCanvas()
    {
        if (GameObject.Find("Canvas(Main)"))
            mainCanvas = GameObject.Find("Canvas(Main)");
        else if (GameObject.Find("Canvas"))
            mainCanvas = GameObject.Find("Canvas");
        if (GameObject.Find("Canvas(MainMap)"))
            mainMapCanvas = GameObject.Find("Canvas(MainMap)");
        if (GameObject.Find("Canvas(Battle)"))
            battleCanvas = GameObject.Find("Canvas(Battle)");        
        if (GameObject.Find("Canvas(Shop)"))
            shopCanvas = GameObject.Find("Canvas(Shop)");
    }
    void LoadCamera()
    {
        if (GameObject.Find("MainCamera"))
            cameraManager.mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        if (GameObject.Find("BattleCamera"))
            cameraManager.battleCamera = GameObject.Find("BattleCamera").GetComponent<Camera>();
        if (GameObject.Find("ShopCamera"))
            cameraManager.shopCamera = GameObject.Find("ShopCamera").GetComponent<Camera>();
    }

    void OnMainCanvas()
    {
        mainCanvas.SetActive(true);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(true);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
    }
    void OnBattleCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }
    void OnShopCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }


    void FindMainHpGoldText(TextUI uI)
    {
        if (GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>())
            uI.UIhp = GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>())
            uI.UIGold = GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>();
    }
    void FindBattleHpGoldText(TextUI uI)
    {
        if (GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>())
            uI.UIhp = GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>())
            uI.UIGold = GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>();        
        if (GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>())
            uI.UIhpBarText_Player = GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>())
            uI.UIhpBarText_Enemy = GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>();
    }
    void FindShopHpGoldText(TextUI uI)
    {
        if (GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>())
            uI.UIhp = GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>())
            uI.UIGold = GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>();
    }
    void ShowText(TextUI uI)
    {
        if (uI.UIhp != null && uI.UIGold != null)
        {
            uI.UIhp.text = playerStatus.hp + " / " + playerStatus.maxHp;
            uI.UIGold.text = playerGold + " G";
        }

        if (uI.playerHpBar != null && uI.ememyHpBar != null)
        {
            uI.playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
            uI.ememyHpBar.value = enemyStatus.hp / enemyStatus.maxHp;
        }

        if (uI.UIhpBarText_Player != null && uI.UIhpBarText_Enemy != null)
        {
            uI.UIhpBarText_Player.text = playerStatus.hp + " / " + playerStatus.maxHp;
            uI.UIhpBarText_Enemy.text = enemyStatus.hp + " / " + enemyStatus.maxHp;
        }
    }

    #region Life Cycle Function
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // 카드셋 생성 함수
        MakeEasyCardSet();
        // Canvas, Camera들 Load
        LoadCanvas();
        LoadCamera();

        // player의 Status부여, HP bar 매칭
        if (playerStatus.unitcode == UNIT_TYPE.EMPTY)
            playerStatus = playerStatus.SetUnitStatus(UNIT_TYPE.PLAYER);
        battleUI.playerHpBar = GameObject.Find("Player_Hp_Slider").GetComponent<Slider>();
        battleUI.playerHpBar.value = playerStatus.hp / playerStatus.maxHp;

        // 현재 설정된 난이도에 따른 enemy의 Status수치 부여
        // 이후 Enemy의 HP bar 매칭
        if (difficulty != DIFFICULTY.NONE)
        {
            switch(difficulty)
            {
                case DIFFICULTY.EASY:
                    if (enemyStatus.unitcode == UNIT_TYPE.EMPTY)
                        enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_easy);
                    break;
                case DIFFICULTY.NORMAL:
                    if (enemyStatus.unitcode == UNIT_TYPE.EMPTY)
                        enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_normal);
                    break;
                case DIFFICULTY.HARD:
                    if (enemyStatus.unitcode == UNIT_TYPE.EMPTY)
                        enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_hard);
                    break;
            }
        }
        battleUI.ememyHpBar = GameObject.Find("Enemy_Hp_Slider").GetComponent<Slider>();
        battleUI.ememyHpBar.value = enemyStatus.hp / enemyStatus.maxHp;

        // 각 장면에서 활용된 Hp, Gold 관련 UI를 Find
        FindMainHpGoldText(mainUI);
        FindBattleHpGoldText(battleUI);
        FindShopHpGoldText(shopUI);

        if (GameObject.Find("Card1").GetComponent<CardScript>())
        {
            GameObject.Find("Card1").GetComponent<CardScript>().number = numberCards[3].attack;
        }
        if (GameObject.Find("Card2").GetComponent<CardScript>())
        {                        
            GameObject.Find("Card2").GetComponent<CardScript>().number = numberCards[5].attack;
        }
        if (GameObject.Find("Card3").GetComponent<CardScript>())
        {                        
            GameObject.Find("Card3").GetComponent<CardScript>().number = numberCards[7].attack;
        }

    }

    // Update is called once per frame
    void Update()
    {  
        if (cameraSelect == CAMERA_TYPE.MAIN)
        {
            cameraManager.OnMainCamera();
            OnMainCanvas();
            ShowText(mainUI);
        }

        if (cameraSelect == CAMERA_TYPE.BATTLE)
        {
            cameraManager.OnBattleCamera();
            OnBattleCanvas();
            ShowText(battleUI);
        }

        if(cameraSelect == CAMERA_TYPE.SHOP)
        {
            cameraManager.OnShopCamera();
            OnShopCanvas();
            ShowText(shopUI);
        }
    }
    #endregion
}
