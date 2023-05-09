using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#region ������
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
    REST,
    SECRET,
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

#region ����ü
public class Status
{
    public UNIT_TYPE unitcode;
    public float maxHp;
    public float hp;    
    public float defence;
    public float shield;

    public Status() { unitcode = UNIT_TYPE.EMPTY; }
    public Status(UNIT_TYPE code, float maxhp, float hp, float defence)
    {
        this.unitcode = code;
        this.maxHp = maxhp;
        this.hp = hp;
        this.defence = defence;
        this.shield = 0;
    }
  
    // Object�� UnitCode�� ���� ������ Status�� ��ȯ
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
                status = new Status(unitcode, 45, 45, 7);
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
    public TextMeshProUGUI UIshield;
    public TextMeshProUGUI UIlevel;
    public TextMeshProUGUI UIhpBarText_Player;
    public TextMeshProUGUI UIhpBarText_Enemy;
    public Slider playerHpBar;
    public Slider playerExpBar;
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
    public static int playerLevel = 1;
    public static int playerPower = 0;
    public static int playerExp = 0;
    public int[] level = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public static int[] power = new int[]{ 0, 2, 4, 7, 10 };
    public int[] exp = new int[] { 5, 10, 15, 20, 25, 30, 35, 40, 45 };

    public static float playerDamage = 0;
    public static float enemyDamage = 0;
    public static int turnCount = 1;
    public static bool turnStart = false;
    public static bool reinforce = false;   
    public static bool nowLevelUp = false;
    public static bool canRest = false;
    public static bool getKey = false;
    public static bool canOperation = true;
    public static GameObject button;    

    public static bool inGame = false;
    public static bool inBattle = false;
    public static bool inboss = false;
    public static bool getVictory = false;
    public static bool newGame = true;
    public CameraManager cameraManager;
    public SpriteManager spriteManager;

    public TextUI mainUI = new TextUI();
    public TextUI battleUI = new TextUI();
    public TextUI shopUI = new TextUI();
    public TextUI specialUI = new TextUI();
    public TextUI restUI = new TextUI();
    public TextUI secretUI = new TextUI();
    public TextMeshProUGUI playerResult;
    public TextMeshProUGUI playerBonus;
    public TextMeshProUGUI enemyResult;
    public TextMeshProUGUI playerDefence;
    public TextMeshProUGUI enemyDefence;    
    public TextMeshProUGUI turnText;

    public GameObject mainCanvas;
    public GameObject mainMapCanvas;
    public GameObject battleCanvas;
    public GameObject shopCanvas;
    public GameObject specialCanvas;
    public GameObject restCanvas;
    public GameObject secretCanvas;
    public GameObject battleButton;
    public GameObject restButton;
    public GameObject restButton2;
    public GameObject resultUI;

    public CardSO cardSO;
    public CardSET playerCardSet = new CardSET();
    public SelectedCardSET selectedCardSet = new SelectedCardSET();
    public SelectedCardSET enemyCardSet = new SelectedCardSET();
    //public CardSET enemyCardSet = new CardSET();

    public static GameObject[] difficultyButtons;
    public static GameObject[] inventorys;
    public static bool[] difficultyButtonsAlive = { true, true, true };
    public GameObject[] numberCards;
    public GameObject[] numberCardsText;    
    public GameObject[] operatorCardsText;
    public GameObject[] selectedCardsText;
    public GameObject[] enemyCards;
    public CardTEXT[] cardText = new CardTEXT[5];

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

    // ���̵� ���� �Լ�
    public static void DifficultySetEasy() { difficulty = DIFFICULTY.EASY; }
    public static void DifficultySetNormal() { difficulty = DIFFICULTY.NORMAL; }
    public static void DifficultySetHard() { difficulty = DIFFICULTY.HARD; }

    //void LoadPopUp()
    //{
    //    levelUpPopUp = GameObject.Find("LevelPopUP");
    //    levelUpText = GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>();
    //}
    //void LevelUpPopUp()
    //{
    //    levelUpPopUp.SetActive(true);
    //    levelUpText.text = "������!\n" + "���� " + playerLevel + " ����!";
    //    Time.timeScale = 0;
    //}

    // ������ �� ��� true ��ȯ
    bool LevelUp()
    {
        switch(playerLevel)
        {
            case 1:
                if (playerExp >= exp[0])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;                    
                    playerExp -= exp[0];
                    playerPower++;
                    if (playerPower > 5)
                    { playerPower = 5; }
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 2:
                if (playerExp >= exp[1])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;                                      
                    playerExp -= exp[1];
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 3:
                if (playerExp >= exp[2])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[2];
                    playerPower++;
                    if (playerPower > 5)
                    { playerPower = 5; }
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 4:
                if (playerExp >= exp[3])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[3];
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 5:
                if (playerExp >= exp[4])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[4];
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 6:
                if (playerExp >= exp[5])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[5];
                    playerPower++;
                    if (playerPower > 5)
                    { playerPower = 5; }
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 7:
                if (playerExp >= exp[6])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[6];
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 8:
                if (playerExp >= exp[7])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[7];
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 9:
                if (playerExp >= exp[8])
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    playerLevel++;
                    nowLevelUp = true;
                    playerExp -= exp[8];
                    playerPower++;
                    if(playerPower > 5)
                    { playerPower = 5; }
                    MaxHpUp(3);
                    return true;
                }
                break;
            case 10:
                //Debug.Log("����� �����̴�!");
                break;
            default:
                //Debug.Log("���� ����");
                break;
        }
        return false;
    }
    void MaxHpUp(float plusHp)
    {
        playerStatus.maxHp += plusHp;
        playerStatus.hp += plusHp;
        if(playerStatus.hp > playerStatus.maxHp)
        { playerStatus.hp = playerStatus.maxHp; }
    }
    public static int GetPlayerPower()
    {
        switch(playerPower)
        {
            case 1:
                return power[0];
            case 2:
                return power[1];
            case 3:
                return power[2];
            case 4:
                return power[3];
            case 5:
                return power[4];
        }
        return 0;
    }

    // �׽�Ʈ�� CardSet���� �Լ�(����3��, ��ȣ2��), CardSet�� ���� Clear���� �Լ�
    void MakeCardSet()
    {
        // ī�� List�� ����������� ���� ä���
        if(playerCardSet.numberCards.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (reinforce) // ī�尡 ��ȭ�Ǿ��ٸ� 1 ~ 8
                { 
                    int rand = Random.Range(1, 9);
                    NumberCard card = new NumberCard(i, rand, null);
                    playerCardSet.numberCards.Add(card);
                }
                else // �Ϲݻ��� 0 ~ 6
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
                        card = new OperatorCard("+", OperatorCard.OPERATOR_TYPE.PLUS, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 1:
                        card = new OperatorCard("-", OperatorCard.OPERATOR_TYPE.MINUS, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 2:
                        card = new OperatorCard("X", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                    case 3:
                        card = new OperatorCard("%", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                        playerCardSet.operatorCards.Add(card);
                        break;
                }
            }
        }        
    }
    void ClearCardSet()
    {
        // ī�� ��Ʈ�� ��������� �ʴٸ� ����.
        if (playerCardSet.numberCards.Count != 0)
        {
            playerCardSet.numberCards.Clear();
            playerCardSet.operatorCards.Clear();
        }       
    }
        
    void MakeEnemyCardSet()
    {      
        if (enemyCardSet.numberCard1.count == 0)
        {
            switch (difficulty)
            {
                case DIFFICULTY.EASY:
                    //Debug.Log("����");
                    if (enemyCardSet.numberCard1.count == 0)
                    {
                        enemyCardSet.result = 0;
                        while (enemyCardSet.result <= 0 || enemyCardSet.result > 25)
                        {
                            enemyCardSet.ClearSelectedCard();

                            NumberCard enemyCard1 = new NumberCard(1, Random.Range(1, 11), null);
                            NumberCard enemyCard2 = new NumberCard(2, Random.Range(1, 11), null);
                            enemyCardSet.numberCard1 = enemyCard1;
                            enemyCardSet.numberCard2 = enemyCard2;

                            while (enemyCardSet.numberCard1.number < enemyCardSet.numberCard2.number)
                            {
                                enemyCard1 = new NumberCard(0, Random.Range(1, 11), null);
                                enemyCard2 = new NumberCard(1, Random.Range(1, 11), null);
                                enemyCardSet.numberCard1 = enemyCard1;
                                enemyCardSet.numberCard2 = enemyCard2;
                            }

                            OperatorCard card;
                            int rand = Random.Range(0, 4);
                            switch (rand)
                            {
                                case 0:
                                    card = new OperatorCard("+", OperatorCard.OPERATOR_TYPE.PLUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number + enemyCardSet.numberCard2.number;
                                    break;
                                case 1:
                                    card = new OperatorCard("-", OperatorCard.OPERATOR_TYPE.MINUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number - enemyCardSet.numberCard2.number;
                                    break;
                                case 2:
                                    card = new OperatorCard("X", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number * enemyCardSet.numberCard2.number;
                                    break;
                                case 3:
                                    card = new OperatorCard("%", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number / enemyCardSet.numberCard2.number;
                                    break;
                            }

                            if (enemyCardSet.result >= 25)
                            { 
                                //Debug.Log("�� ī�� ����. ����: " + enemyCardSet.result); 
                            }
                        }
                    }
                    break;
                case DIFFICULTY.NORMAL:
                    //Debug.Log("�븻");
                    if (enemyCardSet.numberCard1.count == 0)
                    {
                        enemyCardSet.result = 0;
                        while (enemyCardSet.result < 5 || enemyCardSet.result > 30)
                        {
                            enemyCardSet.ClearSelectedCard();

                            NumberCard enemyCard1 = new NumberCard(1, Random.Range(1, 11), null);
                            NumberCard enemyCard2 = new NumberCard(2, Random.Range(1, 11), null);
                            enemyCardSet.numberCard1 = enemyCard1;
                            enemyCardSet.numberCard2 = enemyCard2;

                            while (enemyCardSet.numberCard1.number < enemyCardSet.numberCard2.number)
                            {
                                enemyCard1 = new NumberCard(0, Random.Range(1, 11), null);
                                enemyCard2 = new NumberCard(1, Random.Range(1, 11), null);
                                enemyCardSet.numberCard1 = enemyCard1;
                                enemyCardSet.numberCard2 = enemyCard2;
                            }

                            OperatorCard card;
                            int rand = Random.Range(0, 4);
                            switch (rand)
                            {
                                case 0:
                                    card = new OperatorCard("+", OperatorCard.OPERATOR_TYPE.PLUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number + enemyCardSet.numberCard2.number;
                                    break;
                                case 1:
                                    card = new OperatorCard("-", OperatorCard.OPERATOR_TYPE.MINUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number - enemyCardSet.numberCard2.number;
                                    break;
                                case 2:
                                    card = new OperatorCard("X", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number * enemyCardSet.numberCard2.number;
                                    break;
                                case 3:
                                    card = new OperatorCard("%", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number / enemyCardSet.numberCard2.number;
                                    break;
                            }

                            if (enemyCardSet.result >= 40 || enemyCardSet.result < 10)
                            { 
                                //Debug.Log("�� ī�� ����. ����: " + enemyCardSet.result);
                            }
                        }
                    }
                    break;
                case DIFFICULTY.HARD:
                    //Debug.Log("�ϵ�");
                    if (enemyCardSet.numberCard1.count == 0)
                    {
                        enemyCardSet.result = 0;
                        while (enemyCardSet.result < 10 || enemyCardSet.result > 40)
                        {
                            enemyCardSet.ClearSelectedCard();

                            NumberCard enemyCard1 = new NumberCard(1, Random.Range(1, 11), null);
                            NumberCard enemyCard2 = new NumberCard(2, Random.Range(1, 11), null);
                            enemyCardSet.numberCard1 = enemyCard1;
                            enemyCardSet.numberCard2 = enemyCard2;

                            while (enemyCardSet.numberCard1.number < enemyCardSet.numberCard2.number)
                            {
                                enemyCard1 = new NumberCard(0, Random.Range(1, 11), null);
                                enemyCard2 = new NumberCard(1, Random.Range(1, 11), null);
                                enemyCardSet.numberCard1 = enemyCard1;
                                enemyCardSet.numberCard2 = enemyCard2;
                            }

                            OperatorCard card;
                            int rand = Random.Range(0, 4);
                            switch (rand)
                            {
                                case 0:
                                    card = new OperatorCard("+", OperatorCard.OPERATOR_TYPE.PLUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number + enemyCardSet.numberCard2.number;
                                    break;
                                case 1:
                                    card = new OperatorCard("-", OperatorCard.OPERATOR_TYPE.MINUS, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number - enemyCardSet.numberCard2.number;
                                    break;
                                case 2:
                                    card = new OperatorCard("X", OperatorCard.OPERATOR_TYPE.MULTIPLY, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number * enemyCardSet.numberCard2.number;
                                    break;
                                case 3:
                                    card = new OperatorCard("%", OperatorCard.OPERATOR_TYPE.DIVIDE, null);
                                    enemyCardSet.operatorCard = card;
                                    enemyCardSet.result = enemyCardSet.numberCard1.number / enemyCardSet.numberCard2.number;
                                    break;
                            }

                            if (enemyCardSet.result >= 40 || enemyCardSet.result < 10)
                            { 
                                //Debug.Log("�� ī�� ����. ����: " + enemyCardSet.result); 
                            }
                        }
                    }
                    break;
            }
        }
    }
           
    // Scene�� �ִ� Tag�� Card, SelectedCard, EnemyCard�� GameObject���� Load
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
        // ���� ī�尡 ��������� �ʴٸ� ����
        if(playerCardSet.numberCards.Count != 0)
        {
            // ��� Object���� TextMeshProUGUI�� �ҷ���
            for(int i=0; i< playerCardSet.numberCards.Count; i++)
            {
                cardText[i] = new CardTEXT();
                cardText[i].cardText = numberCardsText[i].GetComponent<TextMeshProUGUI>();
                if (i < 2)
                { cardText[i].operatorCardText = operatorCardsText[i].GetComponent<TextMeshProUGUI>(); }                
                cardText[i].selectedCardText = selectedCardsText[i].GetComponent<TextMeshProUGUI>();
                cardText[i].enemyCardText = enemyCards[i].GetComponent<TextMeshProUGUI>();
            }

            // �÷��̾��� ī����� Text�� Write
            for (int i = 0; i < playerCardSet.numberCards.Count; i++)
            {
                cardText[i].cardText.text = playerCardSet.numberCards[i].number.ToString();
                if (i < 2)
                { cardText[i].operatorCardText.text = playerCardSet.operatorCards[i].name;  }
                cardText[i].selectedCardText.text = "empty";                
            }

            // ���� ī�� Text�� Write
            for(int i=0; i<=3; i++)
            {
                if (i == 0)
                { cardText[0].enemyCardText.text = enemyCardSet.numberCard1.number.ToString(); }
                else
                { cardText[1].enemyCardText.text = enemyCardSet.operatorCard.name; }
                if (i == 2)
                { cardText[2].enemyCardText.text = enemyCardSet.numberCard2.number.ToString(); }                
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
                    //Debug.Log("���� ����");
                    break;
            }
        }
        
        // ������ ī���� �����ڸ� SelectedCardObject�� Text�� ���
        switch(selectOperatorCard)
        {
            case PLAYER_OPERATOR.FIRST:
                selectedCardSet.operatorCard = playerCardSet.operatorCards[0];
                break;
            case PLAYER_OPERATOR.SECOND:
                selectedCardSet.operatorCard = playerCardSet.operatorCards[1];
                break;
            default:
                //Debug.Log("���� ����");
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
                    //Debug.Log("���� ����");
                    break;
            }

            if (divideCheck() || minusCheck())
            { 
                battleButton.SetActive(false);
                resultUI.SetActive(false);
            }
            else
            {
                battleButton.SetActive(true);
                resultUI.SetActive(true);
            }
        }
    }
    bool divideCheck()
    {
        if(selectedCardSet.operatorCard.type == OperatorCard.OPERATOR_TYPE.DIVIDE)
        {
            if(selectedCardSet.numberCard1.number == 0 || selectedCardSet.numberCard2.number == 0)
            {
                canOperation = false;
                MakeCardSet();
                //Debug.Log("0�� ������ �Ұ�!");                
                return true;
            }
            if(selectedCardSet.numberCard1.number < selectedCardSet.numberCard2.number)
            {
                canOperation = false;
                MakeCardSet();
                //Debug.Log("�� ū���� ������ �ִ�");
                return true;
            }
        }
        return false;
    }
    bool minusCheck()
    {
        if (selectedCardSet.operatorCard.type == OperatorCard.OPERATOR_TYPE.MINUS)
        {
            if (selectedCardSet.numberCard1.number < selectedCardSet.numberCard2.number)
            {
                canOperation = false;
                MakeCardSet();
                //Debug.Log("�� ū���� �����ϰ� �ֽ��ϴ�!");
                //Debug.Log("�ٽ� ������!");
                return true;
            }
        }
        return false;
    }

    void NextTurn()
    {
        selectNumberCard = PLAYER_CARD.NOT;
        selectOperatorCard = PLAYER_OPERATOR.NOT;
        selectedCardCount = SELECTED_CARD_COUNT.FIRST;
        playerDamage = 0;
        enemyDamage = 0;
       // Debug.Log(numberCards.Length);        
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
                //Debug.Log("����");
                enemyStatus = enemyStatus.SetUnitStatus(UNIT_TYPE.enemy_easy);
                break;
            case DIFFICULTY.NORMAL:
                //Debug.Log("�븻");
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
                // ������ ������ ���̵��� ������ [ù��°���� - �ι�°����] ����
                if (difficulty != DIFFICULTY.EASY)
                {
                    result = selectedCardSet.numberCard1.number - selectedCardSet.numberCard2.number;
                    break;
                }
                // ������ ��� [ù��°����]�� [�ι�°����]���� Ŭ ��쿡�� ����
                if (selectedCardSet.numberCard1.number >= selectedCardSet.numberCard2.number)
                { result = selectedCardSet.numberCard1.number - selectedCardSet.numberCard2.number; } 
                break;
            case OperatorCard.OPERATOR_TYPE.MULTIPLY:
                result = selectedCardSet.numberCard1.number * selectedCardSet.numberCard2.number;
                break;
            case OperatorCard.OPERATOR_TYPE.DIVIDE:
                if(selectedCardSet.numberCard1.number != 0 && selectedCardSet.numberCard2.number != 0)
                { result = selectedCardSet.numberCard1.number / selectedCardSet.numberCard2.number; }                
                break;
            default:
                //Debug.Log("���� ����");
                break;
        }

        return result;
        
    }
    void WriteResultText()
    {
        float result = CalculateResult();

        if (GameObject.Find("PlayerResultText"))
        {
            playerResult = GameObject.Find("PlayerResultText").GetComponent<TextMeshProUGUI>();
            playerResult.text = result.ToString();
        }
        if (GameObject.Find("PlayerBonusText"))
        {
            playerBonus = GameObject.Find("PlayerBonusText").GetComponent<TextMeshProUGUI>();
            playerBonus.text = playerPower.ToString();
        }

        if (GameObject.Find("EnemyResultText"))
        {
            enemyResult = GameObject.Find("EnemyResultText").GetComponent<TextMeshProUGUI>();
            enemyResult.text = enemyCardSet.result.ToString();
        }         
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

    // selectedCard��° SelectedCard ������Ʈ�� value �Ǵ� name�� Text�� ���
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

    // ���� Scene�� �ִ� Canvas��, Camera���� ��� Load
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
        if (GameObject.Find("Canvas(Rest)"))
            restCanvas = GameObject.Find("Canvas(Rest)");
        if (GameObject.Find("Canvas(SecretRoom)"))
            secretCanvas = GameObject.Find("Canvas(SecretRoom)");
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
        if (GameObject.Find("RestCamera"))
            cameraManager.restCamera = GameObject.Find("RestCamera").GetComponent<Camera>();
        if (GameObject.Find("SecretRoomCamera"))
            cameraManager.secretRoomCamera = GameObject.Find("SecretRoomCamera").GetComponent<Camera>();
        //Test
        //if (GameObject.Find("LevelPopUp"))
        //    levelPopUp = GameObject.Find("LevelPopUp");
    }
    public static void OffInventory()
    {
        inventorys = GameObject.FindGameObjectsWithTag("Inventory");
        for(int i=0; i<inventorys.Length; i++)
        {
            inventorys[i].SetActive(false);
        }
    }

    // �Լ� �̸��� �ش��ϴ� Canvas�� Ȱ��ȭ, �̿��� Canvas�� off
    void OnMainCanvas()
    {
        mainCanvas.SetActive(true);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(true);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
        restCanvas.SetActive(false);
        secretCanvas.SetActive(false);
    }
    void OnBattleCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
        restCanvas.SetActive(false);
        secretCanvas.SetActive(false);
    }
    void OnShopCanvas()
    {
        mainCanvas.SetActive(false);
        if(mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        specialCanvas.SetActive(false);
        restCanvas.SetActive(false);
        secretCanvas.SetActive(false);
    }
    void OnSpecialCanvas()
    {
        mainCanvas.SetActive(false);
        if (mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(true);
        restCanvas.SetActive(false);
        secretCanvas.SetActive(false);
    }
    void OnRestCanvas()
    {
        mainCanvas.SetActive(false);
        if (mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
        restCanvas.SetActive(true);
        secretCanvas.SetActive(false);
    }
    void OnSecretRoomCanvas()
    {
        mainCanvas.SetActive(false);
        if (mainMapCanvas)
            mainMapCanvas.SetActive(false);
        battleCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        specialCanvas.SetActive(false);
        restCanvas.SetActive(false);
        secretCanvas.SetActive(true);
    }

    // �� �Լ��� �ش��ϴ� ��Ʈ�� Text���� UI ������Ʈ�� ���ڷ� ���� TextUI������ ����
    void FindMainText(TextUI ui)
    {
        if (GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("MainHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("MainGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("MainShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("MainShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("MainLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("MainLevelText").GetComponent<TextMeshProUGUI>();
    }
    void FindBattleText(TextUI ui)
    {
        if (GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("BattleHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("BattleGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("BattleShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("BattleShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("BattleLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("BattleLevelText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>())
            ui.UIhpBarText_Player = GameObject.Find("Player_Hp_Text").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>())
            ui.UIhpBarText_Enemy = GameObject.Find("Enemy_Hp_Text").GetComponent<TextMeshProUGUI>();
    }
    void FindShopText(TextUI ui)
    {
        if (GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("ShopHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("ShopGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("ShopShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("ShopShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("ShopLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("ShopLevelText").GetComponent<TextMeshProUGUI>();
    }
    void FindSpecialText(TextUI ui)
    {
        if (GameObject.Find("SpecialHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("SpecialHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SpecialGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("SpecialGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SpecialShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("SpecialShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SpecialLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("SpecialLevelText").GetComponent<TextMeshProUGUI>();
    }
    void FindRestText(TextUI ui)
    {
        if (GameObject.Find("RestHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("RestHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("RestGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("RestGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("RestShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("RestShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("RestLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("RestLevelText").GetComponent<TextMeshProUGUI>();
    }
    void FindSecretText(TextUI ui)
    {
        if (GameObject.Find("SecretHPText").GetComponent<TextMeshProUGUI>())
            ui.UIhp = GameObject.Find("SecretHPText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SecretGoldText").GetComponent<TextMeshProUGUI>())
            ui.UIGold = GameObject.Find("SecretGoldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SecretShieldText").GetComponent<TextMeshProUGUI>())
            ui.UIshield = GameObject.Find("SecretShieldText").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("SecretLevelText").GetComponent<TextMeshProUGUI>())
            ui.UIlevel = GameObject.Find("SecretLevelText").GetComponent<TextMeshProUGUI>();
    }

    // ���ڷ� ���� TextUI�� ������ Hp, Gold, Slider������ ���
    void ShowHpGoldText(TextUI ui)
    {
        if (ui.UIhp != null && ui.UIGold != null)
        {
            ui.UIhp.text = playerStatus.hp + " / " + playerStatus.maxHp;
            ui.UIGold.text = playerGold + " G";
            ui.UIshield.text = playerStatus.shield.ToString();
            ui.UIlevel.text = playerLevel.ToString();
        }

        if (ui.playerHpBar != null && ui.ememyHpBar != null)
        {
            ui.playerHpBar.value = Mathf.Lerp(ui.playerHpBar.value, playerStatus.hp / playerStatus.maxHp, Time.deltaTime * 10);
            ui.ememyHpBar.value = Mathf.Lerp(ui.ememyHpBar.value, enemyStatus.hp / enemyStatus.maxHp, Time.deltaTime * 10);
        }

        if (ui.playerExpBar != null)
        {            
            ui.playerExpBar.value = Mathf.Lerp(ui.playerExpBar.value, (float)playerExp / (float)exp[playerLevel - 1], Time.deltaTime * 10);
        }

        if (ui.UIhpBarText_Player != null && ui.UIhpBarText_Enemy != null)
        {
            ui.UIhpBarText_Player.text = playerStatus.hp + " / \n" + playerStatus.maxHp;
            ui.UIhpBarText_Enemy.text = enemyStatus.hp + " / \n" + enemyStatus.maxHp;
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
            turnText.text = turnCount + " ��";
        }
    }

    void BattleClear()
    {
        for (int i = 0; i < numberCards.Length; i++)
        {
            numberCards[i].GetComponent<Button>().interactable = true;
        }
        playerCardSet.ClearCardSET();
        enemyCardSet.ClearSelectedCard();
        selectedCardSet.ClearSelectedCard();
        turnStart = false;
        battleButton.SetActive(false);
        resultUI.SetActive(false);
       // Debug.Log("Battle Clear");
    }

    #region Life Cycle Function
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // ī��� ���� �Լ�
        // Canvas, Camera�� Load
        LoadCanvas();
        LoadCamera();
        AudioManager.instance.PlayBgm(true);

        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.Equals("1.StoryScene"))
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
            // player�� Status�ο�, HP bar ��Ī
            if (playerStatus.unitcode == UNIT_TYPE.EMPTY)
                playerStatus = playerStatus.SetUnitStatus(UNIT_TYPE.PLAYER);
        }

        if (inGame)
        {            
            battleUI.playerHpBar = GameObject.Find("Player_Hp_Slider").GetComponent<Slider>();
            battleUI.playerExpBar = GameObject.Find("Player_Exp_Slider_B").GetComponent<Slider>();
            mainUI.playerExpBar = GameObject.Find("Player_Exp_Slider_M").GetComponent<Slider>();
            battleUI.playerHpBar.value = playerStatus.hp / playerStatus.maxHp;
            mainUI.playerExpBar.value = (float)playerExp / (float)exp[playerLevel - 1];

            // ���� ������ ���̵��� ���� enemy�� Status��ġ �ο�
            // ���� Enemy�� HP bar ��Ī
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
            restButton = GameObject.Find("RestButton");
            restButton2 = GameObject.Find("RestButton2");
            restButton.GetComponent<Button>().interactable = false;
            restButton2.GetComponent<Button>().interactable = false;
            resultUI = GameObject.FindGameObjectWithTag("ResultUI");
            resultUI.SetActive(false);

            // �� ��鿡�� Ȱ��� Hp, Gold ���� UI�� Find
            FindMainText(mainUI);
            FindBattleText(battleUI);
            FindShopText(shopUI);
            FindSpecialText(specialUI);
            FindRestText(restUI);
            FindSecretText(secretUI);
            FindTurnUI();

            ClearCardSet();
            enemyCardSet.ClearSelectedCard();
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
                selectedCardSet.ClearSelectedCard();
                BattleStateClear();
                inBattle = true;
                turnCount = 1;
            }
            if (getVictory)
            {                
                while (LevelUp())
                {  }
                BattleClear();
                getVictory = false;
                if (nowLevelUp == false)
                {
                    cameraSelect = CAMERA_TYPE.MAIN;
                }
            }
            if (turnStart)
            {                
                turnCount++;
                BattleClear();
                NextTurn();
            }
            // �� Camera�� MainCamera�� ����
            if (cameraSelect == CAMERA_TYPE.MAIN)
            {
                cameraManager.OnMainCamera();
                OnMainCanvas();
                ShowHpGoldText(mainUI);
                if(canRest)
                { 
                    if(restButton)
                    {
                        restButton.GetComponent<Button>().interactable = true;
                    }
                    if (restButton2)
                    {
                        restButton2.GetComponent<Button>().interactable = true;
                    }
                }
            }
            // �� Camera�� BattleCamera�� ����
            if (cameraSelect == CAMERA_TYPE.BATTLE)
            {                
                inBattle = true;
                cameraManager.OnBattleCamera();
                MakeEnemyCardSet();
                MakeCardSet();
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
            // �� Camera�� ShopCamera�� ����
            if (cameraSelect == CAMERA_TYPE.SHOP)
            {
                cameraManager.OnShopCamera();
                OnShopCanvas();
                ShowHpGoldText(shopUI);
            }
            // �� Camera�� SpecialCamera�� ����
            if (cameraSelect == CAMERA_TYPE.SPECIAL)
            {
                cameraManager.OnSpecialCamera();
                OnSpecialCanvas();
                ShowHpGoldText(specialUI);
            }
            if (cameraSelect == CAMERA_TYPE.REST)
            {
                cameraManager.OnRestCamera();
                OnRestCanvas();
                ShowHpGoldText(restUI);
            }
            if (cameraSelect == CAMERA_TYPE.SECRET)
            {
                cameraManager.OnSecretRoomCamera();
                OnSecretRoomCanvas();
                ShowHpGoldText(secretUI);
            }
        }       
    }
    #endregion
}
