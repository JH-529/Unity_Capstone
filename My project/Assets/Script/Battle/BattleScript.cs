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
        //Debug.Log(aniName + "üũ");
        return playerAnim.GetCurrentAnimatorStateInfo(0).IsName(aniName) && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f;
    }

    bool EnemyAnimationEnd(string aniName)
    {
        //Debug.Log(aniName + "üũ");
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
            //Debug.Log("���� ��");
        }        

        if (isBlock && AnimationEnd("Block"))
        {
            playerAnim.SetBool("Block", false);
            isBlock = false;
            //Debug.Log("��� ��");
        }
             
        if (enemyAttack && EnemyAnimationEnd("Attack"))
        {
            enemyAnim.SetBool("Attack", false);
            enemyAttack = false;
            //Debug.Log("���� ��");
        }

        //if (GameManager.inBattle && GameManager.turnCount == 1)
        //{
        //    //enemyAnim.SetBool("Die", false);
        //    Debug.Log("����");
        //}
    }    

    // �÷��̾��� ���� ���� �Լ�
    // �÷��̾� ���� ���� �� EnemyAttack() �ڵ� ����
    // �� óġ�� �¸�ó�� ����
    public void PlayerAttack()
    {
        playerAnim.SetBool("Attack", true);
        isAttack = true;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);             

        attackButton.interactable = false;
        defenceButton.interactable = false;

        // �������� �߰� ������ ����
        for (int i = 0; i < items.Count; i++)
        {
            bonusDamage += items[i].attack;
            //Debug.Log("���ݷ� " + items[i].attack + " �߰�");
        }

        float damage = GameManager.playerDamage + GameManager.GetPlayerPower();        
        damage += bonusDamage;
        bonusDamage = 0;

        // ������ ������ ��� ���� �켱 ����
        // �� ������(0�̸����� ����) �׶����� hp ����
        if (GameManager.enemyStatus.defence > 0)
        {
            GameManager.enemyStatus.defence -= damage;
            if (GameManager.enemyStatus.defence < 0)
            {
                // �� �ʰ��� ���������� hp ����, defence�� 0���� �ʱ�ȭ
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
            Debug.Log("����");
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

    // ���� ���� �Լ�
    // ���� ���� �� EnemyAttack() �ڵ� ����
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

    // ���� ���� ���� �Լ�
    // ���� �������� �÷��̾� ����� ó���ϴ� ��� ����
    public void EnemyAttack()
    {
        enemyAnim.SetBool("Attack", true);
        enemyAttack = true;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);
        //playerAnim.SetBool("Attack", false);
        //playerAnim.SetBool("Block", false);

        // �������� �߰� �� ����
        for (int i = 0; i < items.Count; i++)
        {
            bonusDefence += items[i].defence;
           // Debug.Log("���� " + items[i].defence + " �߰�");
        }

        float damage = GameManager.enemyDamage;
        damage -= bonusDefence;
        if (damage <= 0)
        { damage = 0; }
        bonusDefence = 0;

        // ������ ������ ��� ���� �켱 ����
        // �� ������(0�̸����� ����) �׶����� hp ����
        if (GameManager.playerStatus.defence > 0)
        {
            GameManager.playerStatus.defence -= damage;
            if(GameManager.playerStatus.defence < 0)
            {
                // �� �ʰ��� ���������� hp ����, defence�� 0���� �ʱ�ȭ
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
    // �븻, �ϵ� ��� ����, �� �� ��ġ ���� �ʿ�
    void Victory()
    {       
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        attackButton.interactable = true;
        defenceButton.interactable = true;

        //Debug.Log("�¸�!");
        GameManager.inBattle = false;
        GameManager.getVictory = true;
        GameManager.canRest = true;
       
        // ���� �¸� ����
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
                //Debug.Log(GameManager.turnCount + "�� �ҿ�");
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
               // Debug.Log(GameManager.turnCount + "�� �ҿ�");
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
                //Debug.Log(GameManager.turnCount + "�� �ҿ�");
                break;
        }

    }
    public void KillBoss()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);

        // Debug.Log("���� Ŭ����!");        
        GameManager.inGame = false;
        killBoss = true;
        //SceneManager.LoadScene("1.DifficultyScene");
        //GameManager.cameraSelect = CAMERA_TYPE.MAIN;

        switch (GameManager.difficulty)
        {            
            case DIFFICULTY.EASY:
                //Debug.Log("���� Ŭ����!");
                GameManager.difficultyButtonsAlive[0] = false;
                break;
            case DIFFICULTY.NORMAL:
                //Debug.Log("�븻 Ŭ����!");
                GameManager.difficultyButtonsAlive[1] = false;
                break;
            case DIFFICULTY.HARD:
                //Debug.Log("�ϵ� Ŭ����!");
                GameManager.difficultyButtonsAlive[2] = false;
                break;
            default:
                //Debug.Log("���̵� ���� ����");
                break;
        }
    }
    void Defeat()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Defeat);
        playerAnim.SetBool("Die", true);

        //Debug.Log("�й�..");
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
        //Debug.Log(exp + " ����ġ ȹ��!");
        GameManager.getVictory = true; // ������ �׽�Ʈ��

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
