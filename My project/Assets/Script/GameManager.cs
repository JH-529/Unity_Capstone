using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#region 열거형
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
    SPECIAL,
}
public enum PLAYER_CARD
{
    NOT,
    FIRST,
    SECOND,
    THIRD,
}
public enum PLAYER_OPERATOR
{
    NOT,
    FIRST,
    SECOND,
}
public enum SELECTED_CARD_COUNT
{
    FIRST,
    SECOND,
    THIRD,
}
#endregion

#region 구조체
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
                status = new Status(unitcode, 50, 50, 0);
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

public class CardTEXT
{
    public TextMeshProUGUI cardText;
    public TextMeshProUGUI operatorCardText;
    public TextMeshProUGUI selectedCardText;
    public TextMeshProUGUI enemyCardText;
}

public class CardSET
{
    public List<NumberCard> numberCards;
    public List<OperatorCard> operatorCards;
    public float result;

    public CardSET()
    {
        numberCards = new List<NumberCard>();
        operatorCards = new List<OperatorCard>();
    }

    public void ClearCardSET()
    {
        numberCards = null;
        operatorCards = null;
        System.GC.Collect();
        numberCards = new List<NumberCard>();
        operatorCards = new List<OperatorCard>();
    }
}

public class SelectedCardSET
{
    public NumberCard numberCard1;
    public NumberCard numberCard2;
    public OperatorCard operatorCard;
    public float result;

    public SelectedCardSET()
    {
        numberCard1 = new NumberCard();
        numberCard2 = new NumberCard();
        operatorCard = new OperatorCard();
        result = 0;
    }

    public void ClearSelectedCard()
    {
        numberCard1 = null;
        numberCard2 = null;
        operatorCard = null;
        result = 0;
        System.GC.Collect();
        numberCard1 = new NumberCard();
        numberCard2 = new NumberCard();
        operatorCard = new OperatorCard();
    }
}
#endregion

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static Status playerStatus = new Status();
    public static Status enemyStatus = new Status();
    public static DIFFICULTY difficulty = DIFFICULTY.NONE;
    public static CAMERA_TYPE cameraSelect;
    public static PLAYER_CARD selectNumberCard = PLAYER_CARD.NOT;
    public static PLAYER_OPERATOR selectOperatorCard = PLAYER_OPERATOR.NOT;
    public static SELECTED_CARD_COUNT selectedCardCount = SELECTED_CARD_COUNT.FIRST;
    public static int playerGold = 0;
    public static float playerDamage = 0;
    public static float enemyDamage = 0;
    public static int turnCount = 1;
    public static bool turnStart = false;
    public static bool reinforce = false;
    public static bool inboss = false;
    public static GameObject button;    

    public static bool inGame = false;
    public static bool inBattle = false;
    public static bool newGame = true;
    public CameraManager cameraManager;
    public SpriteManager spriteManager;

    public TextUI mainUI = new TextUI();
    public TextUI battleUI = new TextUI();
    public TextUI shopUI = new TextUI();
    public TextUI specialUI = new TextUI();
    public TextMeshProUGUI playerResult;
    public TextMeshProUGUI enemyResult;
    public TextMeshProUGUI playerDefence;
    public TextMeshProUGUI enemyDefence;    
    public TextMeshProUGUI turnText;

    public GameObject mainCanvas;
    public GameObject mainMapCanvas;
    public GameObject battleCanvas;
    public GameObject shopCanvas;
    public GameObject specialCanvas;
    public GameObject battleButton;

    public CardSO cardSO;
    public CardSET playerCardSet = new CardSET();
    public SelectedCardSET selectedCardSet = new SelectedCardSET();
    public CardSET enemyCardSet = new CardSET();

    public static GameObject[] difficultyButtons;
    public static bool[] difficultyButtonsAlive = { true, true, true };
    public GameObject[] numberCards;
    public GameObject[] numberCardsText;    
    public GameObject[] operatorCardsText;
    public GameObject[] selectedCardsText;
    public GameObject[] enemyCards;
    public CardTEXT[] cardText = new CardTEXT[5];

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

    // 난이도 세팅 함수
    public static void DifficultySetEasy() { difficulty = DIFFICULTY.EASY; }
    public static void DifficultySetNormal() { difficulty = DIFFICULTY.NORMAL; }
    public static void DifficultySetHard() { difficulty = DIFFICULTY.HARD; }

    // 테스트용 CardSet생성 함수(숫자3개, 기호2개), CardSet에 대한 Clear수행 함수
    void MakeCardSet()
    {
        // 카드 List가 비워져있으면 새로 채운다
        if(playerCardSet.numberCards.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (reinforce) // 카드가 강화되었다면 1 ~ 8
                { 
                    int rand = Random.Range(1, 9);
                    NumberCard card = new NumberCard(i, rand, null);
                    playerCardSet.numberCards.Add(card);
                }
                else // 일반상태 0 ~ 6
                { 
                    int rand = Random.Range(0, 7);
                    NumberCard card = new NumberCard(i, rand, null);
                    playerCardSet.numberCards.Add(card);
                }
            }

            int prev = 0;
            for (int i = 0; i < 2; i++)
            {
                OperatorCard card;
                int rand = Random.Range(0, 4);
                if(i == 0)
                { prev = rand; }
                else
                {
                    while(prev == rand)
                    {
                        rand = Random.Range(0, 4);
                    }
                }

                switch (rand)
                {
                    case 0:
                        card = new OperatorCard("PLUS", OperatorCard.OPERATOR_TYPE.PLUS, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 1:
                        card = new OperatorCard("MINUS", OperatorCard.OPERATOR_TYPE.MINUS, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 2:
                        card = new OperatorCard("MULTIPLY", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 3:
                        card = new OperatorCard("DIVIDE", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                }
            }
        }        
    }
    void ClearCardSet()
    {
        // 카드 세트가 비워져있지 않다면 비운다.
        if (playerCardSet.numberCards.Count != 0)
        {
            playerCardSet.numberCards.Clear();
            playerCardSet.operatorCards.Clear();
        }       
    }

    void MakeEnemyCardSet()
    {
        if (enemyCardSet.numberCards.Count == 0)
        {
            enemyCardSet.result = 0;
            while (enemyCardSet.result <= 0 || enemyCardSet.result > 30)
            {               
                ClearEnemyCardSet();
                for (int i = 0; i < 2; i++)
                {
                    NumberCard enemyCard = new NumberCard(i, Random.Range(1, 11), null);
                    enemyCardSet.numberCards.Add(enemyCard);
                }

                OperatorCard card;
                int rand = Random.Range(0, 4);
                switch (rand)
                {
                    case 0:
                        card = new OperatorCard("PLUS", OperatorCard.OPERATOR_TYPE.PLUS, null);
                        enemyCardSet.operatorCards.Add(card);
                        enemyCardSet.result = enemyCardSet.numberCards[0].number + enemyCardSet.numberCards[1].number;
                        break;
                    case 1:
                        card = new OperatorCard("MINUS", OperatorCard.OPERATOR_TYPE.MINUS, null);
                        enemyCardSet.operatorCards.Add(card);
                        enemyCardSet.result = enemyCardSet.numberCards[0].number - enemyCardSet.numberCards[1].number;
                        break;
                    case 2:
                        card = new OperatorCard("MULTIPLY", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                        enemyCardSet.operatorCards.Add(card);
                        enemyCardSet.result = enemyCardSet.numberCards[0].number * enemyCardSet.numberCards[1].number;
                        break;
                    case 3:
                        card = new OperatorCard("DIVIDE", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                        enemyCardSet.operatorCards.Add(card);
                        enemyCardSet.result = enemyCardSet.numberCards[0].number / enemyCardSet.numberCards[1].number;
                        break;
                }

                if(enemyCardSet.result >= 30)
                { Debug.Log("적 카드 생성. 현재: " + enemyCardSet.result); }                
            }            
        }       
    }
    void ClearEnemyCardSet()
    {
        if(enemyCardSet.numberCards.Count != 0)
        {
            enemyCardSet.numberCards.Clear();
            enemyCardSet.operatorCards.Clear();
        }        
    }
        
    // Scene에 있는 Tag가 Card, SelectedCard, EnemyCard인 GameObject들을 Load
    void LoadCardGameObject()
    {
        numberCards = GameObject.FindGameObjectsWithTag("Card");
        numberCardsText = GameObject.FindGameObjectsWithTag("CardText");
        operatorCardsText = GameObject.FindGameObjectsWithTag("OperatorCardText");
        selectedCardsText = GameObject.FindGameObjectsWithTag("SelectedCardText");
        enemyCards = GameObject.FindGameObjectsWithTag("EnemyCardText");
    }
    void WriteCardText()
    {
        // 숫자 카드가 비워져있지 않다면 수행
        if(playerCardSet.numberCards.Count != 0)
        {
            // 모든 Object들의 TextMeshProUGUI를 불러옴
            for(int i=0; i< playerCardSet.numberCards.Count; i++)
            {
                cardText[i] = new CardTEXT();
                cardText[i].cardText = numberCardsText[i].GetComponent<TextMeshProUGUI>();
                if (i < 2)
                { cardText[i].operatorCardText = operatorCardsText[i].GetComponent<TextMeshProUGUI>(); }                
                cardText[i].selectedCardText = selectedCardsText[i].GetComponent<TextMeshProUGUI>();
                cardText[i].enemyCardText = enemyCards[i].GetComponent<TextMeshProUGUI>();
            }

            // 플레이어의 카드들의 Text를 Write
            for (int i = 0; i < playerCardSet.numberCards.Count; i++)
            {
                cardText[i].cardText.text = playerCardSet.numberCards[i].number.ToString();
                if (i < 2)
                { cardText[i].operatorCardText.text = playerCardSet.operatorCards[i].name;  }
                cardText[i].selectedCardText.text = "empty";                
            }

            // 적의 카드 Text를 Write
            for(int i=0; i<=enemyCardSet.numberCards.Count; i++)
            {
                if (i == 0)
                { cardText[0].enemyCardText.text = enemyCardSet.numberCards[0].number.ToString(); }
                else
                { cardText[1].enemyCardText.text = enemyCardSet.operatorCards[0].name; }
                if (i == 2)
                { cardText[2].enemyCardText.text = enemyCardSet.numberCards[1].number.ToString(); }                
            }
        }
    }

    void CardSelectFunction()
    {
        if (selectedCardCount == SELECTED_CARD_COUNT.FIRST)
        {
            switch (selectNumberCard)
            {
                case PLAYER_CARD.FIRST:
                    selectedCardSet.numberCard1 = playerCardSet.numberCards[0];
                    break;
                case PLAYER_CARD.SECOND:
                    selectedCardSet.numberCard1 = playerCardSet.numberCards[1];
                    break;
                case PLAYER_CARD.THIRD:
                    selectedCardSet.numberCard1 = playerCardSet.numberCards[2];
                    break;
                default:
                    Debug.Log("선택 오류");
                    break;
            }
        }
        
        // 선택한 카드의 연산자를 SelectedCardObject의 Text로 출력
        switch(selectOperatorCard)
        {
            case PLAYER_OPERATOR.FIRST:
                selectedCardSet.operatorCard = playerCardSet.operatorCards[0];
                break;
            case PLAYER_OPERATOR.SECOND:
                selectedCardSet.operatorCard = playerCardSet.operatorCards[1];
                break;
            default:
                Debug.Log("선택 오류");
                break;
        }

        if (selectedCardCount == SELECTED_CARD_COUNT.THIRD)
        {            
            switch (selectNumberCard)
            {
                case PLAYER_CARD.FIRST:
                    selectedCardSet.numberCard2 = playerCardSet.numberCards[0];       
                    break;
                case PLAYER_CARD.SECOND:
                    selectedCardSet.numberCard2 = playerCardSet.numberCards[1];
                    break;
                case PLAYER_CARD.THIRD:
                    selectedCardSet.numberCard2 = playerCardSet.numberCards[2];
                    break;
                default:
                    Debug.Log("선택 오류");
                    break;
            }
            if(divideCheck())
            { battleButton.SetActive(false); }
            else
            { battleButton.SetActive(true); }
        }
    }

    bool divideCheck()
    {
        if(selectedCardSet.operatorCard.type == OperatorCard.OPERATOR_TYPE.DIVIDE)
        {
            if(selectedCardSet.numberCard1.number == 0 || selectedCardSet.numberCard2.number == 0)
            {
                Debug.Log("0은 나누기 불가!");
                return true;
            }
            if(selectedCardSet.numberCard1.number < selectedCardSet.numberCard2.number)
            {
                Debug.Log("더 큰수로 나누고 있다");
                return true;
            }
        }
        return false;
    }

    void NextTurn()
    {
        playerCardSet.ClearCardSET();
        enemyCardSet.ClearCardSET();
        selectNumberCard = PLAYER_CARD.NOT;
        selectOperatorCard = PLAYER_OPERATOR.NOT;
        selectedCardCount = SELECTED_CARD_COUNT.FIRST;
        playerDamage = 0;
        enemyDamage = 0;
        Debug.Log(numberCards.Length);
        for (int i = 0; i < numberCards.Length; i++)
        {
            numberCards[i].GetComponent<Button>().interactable = true;
        }
    }
    void BattleStateClear()
    {
        selectNumberCard = PLAYER_CARD.NOT;
        selectOperatorCard = PLAYER_OPERATOR.NOT;
        selectedCardCount = SELECTED_CARD_COUNT.FIRST;
        playerDamage = 0;
        enemyDamage = 0;
        turnCount = 1;
        switch (difficulty)
        {
            case DIFFICULTY.EASY:
                    enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_easy);
                break;
            case DIFFICULTY.NORMAL:
                    enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_normal);
                break;
            case DIFFICULTY.HARD:
                    enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_hard);
                break;
        }
        
    }
    float CalculateResult()
    {
        OperatorCard.OPERATOR_TYPE type = selectedCardSet.operatorCard.type;
        float result = 0;

        switch (type)
        {
            case OperatorCard.OPERATOR_TYPE.PLUS:
                result = selectedCardSet.numberCard1.number + selectedCardSet.numberCard2.number;
                break;
            case OperatorCard.OPERATOR_TYPE.MINUS:
                result = selectedCardSet.numberCard1.number - selectedCardSet.numberCard2.number;
                break;
            case OperatorCard.OPERATOR_TYPE.MULTIPLY:
                result = selectedCardSet.numberCard1.number * selectedCardSet.numberCard2.number;
                break;
            case OperatorCard.OPERATOR_TYPE.DIVIDE:
                if(selectedCardSet.numberCard1.number != 0 && selectedCardSet.numberCard2.number != 0)
                { result = selectedCardSet.numberCard1.number / selectedCardSet.numberCard2.number; }                
                break;
            default:
                //Debug.Log("연산 오류");
                break;
        }

        return result;
        
    }
    void WriteResultText()
    {
        if (GameObject.Find("PlayerResultText"))
            playerResult = GameObject.Find("PlayerResultText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("EnemyResultText"))
            enemyResult = GameObject.Find("EnemyResultText").GetComponent<TextMeshProUGUI>();

        float result = CalculateResult();
        playerResult.text = result.ToString();
        enemyResult.text = enemyCardSet.result.ToString();
        playerDamage = result;
        enemyDamage = enemyCardSet.result;
    }
    void WriteDefenceText()
    {
        if (GameObject.Find("PlayerDefenceText"))
            playerDefence = GameObject.Find("PlayerDefenceText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("EnemyDefenceText"))
            enemyDefence = GameObject.Find("EnemyDefenceText").GetComponent<TextMeshProUGUI>();

        playerDefence.text = playerStatus.defence.ToString();
        enemyDefence.text = enemyStatus.defence.ToString();
    }

    // selectedCard번째 SelectedCard 오브젝트에 value 또는 name을 Text로 출력
    void WriteSelectedCardText(SELECTED_CARD_COUNT selectedCard, int value, string name)
    {        
        switch (selectedCard)
        {
            case SELECTED_CARD_COUNT.FIRST:
                cardText[0].selectedCardText.text = value.ToString();
                break;
            case SELECTED_CARD_COUNT.SECOND:
                cardText[1].selectedCardText.text = name;
                break;
            case SELECTED_CARD_COUNT.THIRD:
                cardText[2].selectedCardText.text = value.ToString();
                break;
        }
    }
    void WriteSelectedCardText(NumberCard number1, OperatorCard oper, NumberCard number2)
    {
        if(number1 != null)
        { cardText[0].selectedCardText.text = number1.number.ToString(); }
        if(oper != null)
        { cardText[1].selectedCardText.text = oper.name; }
        if(number2 != null)
        { cardText[2].selectedCardText.text = number2.number.ToString(); }
    }

    // 현재 Scene에 있는 Canvas들, Camera들을 모두 Load
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
        if (GameObject.Find("Canvas(Special)"))
            specialCanvas = GameObject.Find("Canvas(Special)");
    }
    void LoadCamera()
    {
        if (GameObject.Find("MainCamera"))
            cameraManager.mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        if (GameObject.Find("BattleCamera"))
            cameraManager.battleCamera = GameObject.Find("BattleCamera").GetComponent<Camera>();
        if (GameObject.Find("ShopCamera"))
            cameraManager.shopCamera = GameObject.Find("ShopCamera").GetComponent<Camera>();
        if (GameObject.Find("SpecialCamera"))
            cameraManager.specialCamera = GameObject.Find("SpecialCamera").GetComponent<Camera>();
    }

    // 함수 이름에 해당하는 Canvas를 활성화, 이외의 Canvas는 off
    void OnMainCanvas()
    {
        mainCanvas.SetActive(true);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(true);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
    }
    void OnBattleCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
    }
    void OnShopCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        specialCanvas.SetActive(false);
    }
    void OnSpecialCanvas()
    {
        mainCanvas.SetActive(false);
        if (mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(true);
    }

    // 각 함수에 해당하는 파트의 Text관련 UI 오브젝트를 인자로 받은 TextUI변수에 저장
    void FindMainHpGoldText(TextUI ui)
    {
        if (GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>();
    }
    void FindBattleHpGoldText(TextUI ui)
    {
        if (GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>();        
        if (GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>())
            ui.UIhpBarText_Player = GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>())
            ui.UIhpBarText_Enemy = GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>();
    }
    void FindShopHpGoldText(TextUI ui)
    {
        if (GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>();
    }
    void FindSpecialHpGoldText(TextUI ui)
    {
        if (GameObject.Find("SpecialHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("SpecialHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SpecialGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("SpecialGoldText").GetComponent<TextMeshProUGUI>();
    }
    // 인자로 받은 TextUI에 현재의 Hp, Gold, Slider정보를 출력
    void ShowHpGoldText(TextUI ui)
    {
        if (ui.UIhp != null && ui.UIGold != null)
        {
            ui.UIhp.text = playerStatus.hp + " / " + playerStatus.maxHp;
            ui.UIGold.text = playerGold + " G";
        }

        if (ui.playerHpBar != null && ui.ememyHpBar != null)
        {
            ui.playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
            ui.ememyHpBar.value = enemyStatus.hp / enemyStatus.maxHp;
        }

        if (ui.UIhpBarText_Player != null && ui.UIhpBarText_Enemy != null)
        {
            ui.UIhpBarText_Player.text = playerStatus.hp + " / " + playerStatus.maxHp;
            ui.UIhpBarText_Enemy.text = enemyStatus.hp + " / " + enemyStatus.maxHp;
        }
    }
    void FindTurnUI()
    {
        if (GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>())
            turnText = GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>();
    }
    void ShowTurnUI()
    {
        if(turnText)
        {
            turnText.text = turnCount + " Turn";
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
        // Canvas, Camera들 Load
        LoadCanvas();
        LoadCamera();

        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.Equals("1.DifficultyScene"))
        {            
            difficultyButtons = GameObject.FindGameObjectsWithTag("DifficultyButton");
            for(int i=0; i< difficultyButtonsAlive.Length; i++)
            {
                if(!difficultyButtonsAlive[i])
                { difficultyButtons[i].GetComponent<Button>().interactable = false; }
            }
        }

        if (newGame)
        {
            playerGold = 0;
            // player의 Status부여, HP bar 매칭
            if (playerStatus.unitcode == UNIT_TYPE.EMPTY)
                playerStatus = playerStatus.SetUnitStatus(UNIT_TYPE.PLAYER);
        }

        if (inGame)
        {            
            battleUI.playerHpBar = GameObject.Find("Player_Hp_Slider").GetComponent<Slider>();
            battleUI.playerHpBar.value = playerStatus.hp / playerStatus.maxHp;

            // 현재 설정된 난이도에 따른 enemy의 Status수치 부여
            // 이후 Enemy의 HP bar 매칭
            if (difficulty != DIFFICULTY.NONE)
            {
                switch (difficulty)
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

            battleButton = GameObject.FindGameObjectWithTag("BattleButton");
            battleButton.SetActive(false);

            // 각 장면에서 활용된 Hp, Gold 관련 UI를 Find
            FindMainHpGoldText(mainUI);
            FindBattleHpGoldText(battleUI);
            FindShopHpGoldText(shopUI);
            FindSpecialHpGoldText(specialUI);
            FindTurnUI();

            ClearCardSet();
            ClearEnemyCardSet();
            selectedCardCount = SELECTED_CARD_COUNT.FIRST;
            inboss = false;
        }
    }

    // Update is called once per frame
    void Update()
    {  
        if(inGame)
        {
            if(!inBattle)
            {
                Debug.Log("초기화");
                selectedCardSet.ClearSelectedCard();
                BattleStateClear();
                inBattle = true;
                turnCount = 1;
            }
            if(turnStart)
            {
                Debug.Log("다음턴");
                selectedCardSet.ClearSelectedCard();
                NextTurn();
                turnCount++;
                turnStart = false;
                battleButton.SetActive(false);
            }
            // 현 Camera를 MainCamera로 세팅
            if (cameraSelect == CAMERA_TYPE.MAIN)
            {
                cameraManager.OnMainCamera();
                OnMainCanvas();
                ShowHpGoldText(mainUI);
            }
            // 현 Camera를 BattleCamera로 세팅
            if (cameraSelect == CAMERA_TYPE.BATTLE)
            {                
                inBattle = true;
                cameraManager.OnBattleCamera();
                MakeCardSet();
                MakeEnemyCardSet();
                OnBattleCanvas();
                ShowHpGoldText(battleUI);
                ShowTurnUI();
                LoadCardGameObject();               
                WriteCardText();
                CardSelectFunction();
                WriteSelectedCardText(selectedCardSet.numberCard1, selectedCardSet.operatorCard, selectedCardSet.numberCard2);
                WriteResultText();
                WriteDefenceText();
            }
            // 현 Camera를 ShopCamera로 세팅
            if (cameraSelect == CAMERA_TYPE.SHOP)
            {
                cameraManager.OnShopCamera();
                OnShopCanvas();
                ShowHpGoldText(shopUI);
            }
            // 현 Camera를 SpecialCamera로 세팅
            if (cameraSelect == CAMERA_TYPE.SPECIAL)
            {
                cameraManager.OnSpecialCamera();
                OnSpecialCanvas();
                ShowHpGoldText(specialUI);
            }
        }       
    }
    #endregion
}
