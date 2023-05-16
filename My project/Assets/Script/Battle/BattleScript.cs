using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField]
    private List<Item> items;
    [SerializeField]
    private float bonusDamage = 0;
    private float bonusDefence = 0;
    public static bool killBoss = false;

    bool isAttack = false;    
    bool isBlock = false;
    bool enemyAttack = false;
    [SerializeField] Button attackButton;
    [SerializeField] Button defenceButton;

    [SerializeField] Animator playerAnim;
    [SerializeField] Animator enemyAnim;

    bool AnimationEnd(string aniName)
    {
        //Debug.Log(aniName + "체크");
        return playerAnim.GetCurrentAnimatorStateInfo(0).IsName(aniName) && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f;
    }

    bool EnemyAnimationEnd(string aniName)
    {
        //Debug.Log(aniName + "체크");
        return enemyAnim.GetCurrentAnimatorStateInfo(0).IsName(aniName) && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f;
    }

    void Start()
    {
        if(inventory.items != null)
        {
            items = inventory.items;
        }        
    }

    void Update()
    {
        if (isAttack && AnimationEnd("Attack"))
        {
            playerAnim.SetBool("Attack", false);
            isAttack = false;
            //Debug.Log("공격 끝");
        }        

        if (isBlock && AnimationEnd("Block"))
        {
            playerAnim.SetBool("Block", false);
            isBlock = false;
            //Debug.Log("방어 끝");
        }
             
        if (enemyAttack && EnemyAnimationEnd("Attack"))
        {
            enemyAnim.SetBool("Attack", false);
            enemyAttack = false;
            //Debug.Log("공격 끝");
        }

        //if (GameManager.inBattle && GameManager.turnCount == 1)
        //{
        //    //enemyAnim.SetBool("Die", false);
        //    Debug.Log("ㅋㅋ");
        //}
    }    

    // 플레이어의 공격 수행 함수
    // 플레이어 공격 종료 후 EnemyAttack() 자동 진행
    // 적 처치시 승리처리 포함
    public void PlayerAttack()
    {
        playerAnim.SetBool("Attack", true);
        isAttack = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);             

        attackButton.interactable = false;
        defenceButton.interactable = false;

        // 아이템의 추가 데미지 적용
        for (int i = 0; i < items.Count; i++)
        {
            bonusDamage += items[i].attack;
            //Debug.Log("공격력 " + items[i].attack + " 추가");
        }

        float damage = GameManager.playerDamage + GameManager.GetPlayerPower();        
        damage += bonusDamage;
        bonusDamage = 0;

        // 방어력이 존재할 경우 방어력 우선 감소
        // 방어가 깨지면(0미만으로 감소) 그때부터 hp 감소
        if (GameManager.enemyStatus.defence > 0)
        {
            GameManager.enemyStatus.defence -= damage;
            if (GameManager.enemyStatus.defence < 0)
            {
                // 방어를 초과한 데미지만금 hp 감소, defence는 0으로 초기화
                GameManager.enemyStatus.hp += GameManager.enemyStatus.defence;
                GameManager.enemyStatus.defence = 2;
            }
        }

        else
        { GameManager.enemyStatus.hp -= damage; }

        if (GameManager.enemyStatus.hp < 0)
        {
            enemyAnim.SetBool("DIe", true);
            GameManager.enemyStatus.hp = 0;
            Debug.Log("ㄹㅇ");
        }

        if (GameManager.enemyStatus.hp == 0)
        {            
            if (!GameManager.inboss)
            { Invoke("Victory", 1.4f); }
            else
            { Invoke("KillBoss", 1.4f); }
        }
        else
        {
            Invoke("EnemyAttack", 1f);
        }      
        
    }

    // 방어력 증가 함수
    // 방어력 증가 후 EnemyAttack() 자동 진행
    public void GetDefence()
    {
        playerAnim.SetBool("Block", true);
        isBlock = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Defence);

        attackButton.interactable = false;
        defenceButton.interactable = false;

        float defence = GameManager.playerDamage;
        if(defence < 0)
        { defence = 0; }
        GameManager.playerStatus.defence += defence;
        Invoke("EnemyAttack", 1f);

        //GameManager.turnStart = true;
    }

    // 적의 공격 수행 함수
    // 적의 공격으로 플레이어 사망시 처리하는 기능 포함
    public void EnemyAttack()
    {
        enemyAnim.SetBool("Attack", true);
        enemyAttack = true;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);
        //playerAnim.SetBool("Attack", false);
        //playerAnim.SetBool("Block", false);

        // 아이템의 추가 방어도 적용
        for (int i = 0; i < items.Count; i++)
        {
            bonusDefence += items[i].defence;
           // Debug.Log("방어력 " + items[i].defence + " 추가");
        }

        float damage = GameManager.enemyDamage;
        damage -= bonusDefence;
        if (damage <= 0)
        { damage = 0; }
        bonusDefence = 0;

        // 방어력이 존재할 경우 방어력 우선 감소
        // 방어가 깨지면(0미만으로 감소) 그때부터 hp 감소
        if (GameManager.playerStatus.defence > 0)
        {
            GameManager.playerStatus.defence -= damage;
            if(GameManager.playerStatus.defence < 0)
            {
                // 방어를 초과한 데미지만금 hp 감소, defence는 0으로 초기화
                GameManager.playerStatus.hp += GameManager.playerStatus.defence;
                GameManager.playerStatus.defence = 0;
            }
        }
        else
        { GameManager.playerStatus.hp -= damage; }
        
        if(GameManager.playerStatus.hp < 0)
        { GameManager.playerStatus.hp = 0; }

        if(GameManager.playerStatus.hp == 0)
        {
            Invoke("Defeat", 1f);
        }

        Invoke("NextTurn", 1f);
    }

    void NextTurn()
    {
        GameManager.turnStart = true;
        attackButton.interactable = true;
        defenceButton.interactable = true;
    }

    // TODO
    // 노말, 하드 골드 보상, 턴 수 수치 조절 필요
    void Victory()
    {       
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        attackButton.interactable = true;
        defenceButton.interactable = true;

        //Debug.Log("승리!");
        GameManager.inBattle = false;
        GameManager.getVictory = true;
        GameManager.canRest = true;
       
        // 전투 승리 보상
        switch (GameManager.difficulty)
        {
            case DIFFICULTY.EASY:
                if(GameManager.turnCount <= 3)
                {
                    int eRand = Random.Range(20, 40);
                    GameManager.playerGold += eRand;
                    GetExp(15);
                }
                if (GameManager.turnCount >= 4 && GameManager.turnCount <= 7)
                {
                    int eRand = Random.Range(15, 35);
                    GameManager.playerGold += eRand;
                    GetExp(10);
                }
                if (GameManager.turnCount >=  8)
                {
                    int eRand = Random.Range(5, 25);
                    GameManager.playerGold += eRand;
                    GetExp(5);
                }
                //Debug.Log(GameManager.turnCount + "턴 소요");
                break;
            case DIFFICULTY.NORMAL:
                if (GameManager.turnCount <= 3)
                {
                    int eRand = Random.Range(20, 40);
                    GameManager.playerGold += eRand;
                    GetExp(20);
                }
                if (GameManager.turnCount >= 4 && GameManager.turnCount <= 7)
                {
                    int eRand = Random.Range(15, 35);
                    GameManager.playerGold += eRand;
                    GetExp(15);
                }
                if (GameManager.turnCount >= 8)
                {
                    int eRand = Random.Range(5, 25);
                    GameManager.playerGold += eRand;
                    GetExp(10);
                }
               // Debug.Log(GameManager.turnCount + "턴 소요");
                break;
            case DIFFICULTY.HARD:
                if (GameManager.turnCount <= 3)
                {
                    int eRand = Random.Range(20, 40);
                    GameManager.playerGold += eRand;
                }
                if (GameManager.turnCount >= 4 && GameManager.turnCount <= 7)
                {
                    int eRand = Random.Range(15, 35);
                    GameManager.playerGold += eRand;
                }
                if (GameManager.turnCount >= 8)
                {
                    int eRand = Random.Range(5, 25);
                    GameManager.playerGold += eRand;
                }
                //Debug.Log(GameManager.turnCount + "턴 소요");
                break;
        }

    }
    public void KillBoss()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);

        // Debug.Log("게임 클리어!");        
        GameManager.inGame = false;
        killBoss = true;
        //SceneManager.LoadScene("1.DifficultyScene");
        //GameManager.cameraSelect = CAMERA_TYPE.MAIN;

        switch (GameManager.difficulty)
        {            
            case DIFFICULTY.EASY:
                //Debug.Log("이지 클리어!");
                GameManager.difficultyButtonsAlive[0] = false;
                break;
            case DIFFICULTY.NORMAL:
                //Debug.Log("노말 클리어!");
                GameManager.difficultyButtonsAlive[1] = false;
                break;
            case DIFFICULTY.HARD:
                //Debug.Log("하드 클리어!");
                GameManager.difficultyButtonsAlive[2] = false;
                break;
            default:
                //Debug.Log("난이도 설정 오류");
                break;
        }
    }
    void Defeat()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Defeat);
        playerAnim.SetBool("Die", true);

        //Debug.Log("패배..");
        GameManager.inBattle = false;
        GameManager.inGame = false;
        GameManager.newGame = true;
        SceneManager.LoadScene("0.MainScene");
        GameManager.playerStatus = GameManager.playerStatus.SetUnitStatus(UNIT_TYPE.PLAYER);
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }    

    public void GetExp(int exp)
    {
        GameManager.playerExp += exp;
        //Debug.Log(exp + " 경험치 획득!");
        GameManager.getVictory = true; // 레벨업 테스트용

        if(isAttack)
        {
            playerAnim.SetBool("Attack", false);
            isAttack = false;
        }
        if (isBlock)
        {
            playerAnim.SetBool("Block", false);
            isBlock = false;
        }

    }

    public void KillYou()
    {
        GameManager.playerDamage = 1000;
        PlayerAttack();
    }
}
