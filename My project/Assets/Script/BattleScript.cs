using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{
    // �÷��̾��� ���� ���� �Լ�
    // �÷��̾� ���� ���� �� EnemyAttack() �ڵ� ����
    // �� óġ�� �¸�ó�� ����
    public void PlayerAttack()
    {
        float damage = GameManager.playerDamage;
        if(damage <= 0)
        { damage = 0; }

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
        { GameManager.enemyStatus.hp = 0; }

        if (GameManager.enemyStatus.hp == 0)
        {            
            if(GameManager.inboss)
            { KillBoss(); }
            else
            { Victory(); }
        }
        else
        {
            EnemyAttack();
        }
        
        GameManager.turnStart = true;        
    }

    // ���� ���� �Լ�
    // ���� ���� �� EnemyAttack() �ڵ� ����
    public void GetDefence()
    {
        float defence = GameManager.playerDamage;
        if(defence < 0)
        { defence = 0; }
        GameManager.playerStatus.defence += defence;
        EnemyAttack();

        GameManager.turnStart = true;
    }

    // ���� ���� ���� �Լ�
    // ���� �������� �÷��̾� ����� ó���ϴ� ��� ����
    public void EnemyAttack()
    {
        float damage = GameManager.enemyDamage;
        if(damage <= 0)
        { damage = 0; }

        // ������ ������ ��� ���� �켱 ����
        // �� ������(0�̸����� ����) �׶����� hp ����
        if(GameManager.playerStatus.defence > 0)
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
            Defeat();
        }
    }

    void Victory()
    {
        Debug.Log("�¸�!");
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;

        switch (GameManager.difficulty)
        {
            case DIFFICULTY.EASY:
                int eRand = Random.Range(5, 25);
                GameManager.playerGold += eRand;
                break;
            case DIFFICULTY.NORMAL:
                int nRand = Random.Range(10, 30);
                GameManager.playerGold += nRand;
                break;
            case DIFFICULTY.HARD:
                int hRand = Random.Range(15, 40);
                GameManager.playerGold += hRand;
                break;
        }
    }

    public void KillBoss()
    {
        Debug.Log("���̵� Ŭ����!");
        SceneManager.LoadScene("1.DifficultyScene");
        GameManager.inGame = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;

        switch (GameManager.difficulty)
        {            
            case DIFFICULTY.EASY:
                Debug.Log("���� Ŭ����!");
                GameManager.difficultyButtonsAlive[0] = false;
                break;
            case DIFFICULTY.NORMAL:
                Debug.Log("�븻 Ŭ����!");
                GameManager.difficultyButtonsAlive[1] = false;
                break;
            case DIFFICULTY.HARD:
                Debug.Log("�ϵ� Ŭ����!");
                GameManager.difficultyButtonsAlive[2] = false;
                break;
            default:
                Debug.Log("���̵� ���� ����");
                break;
        }
    }

    void Defeat()
    {
        Debug.Log("�й�..");
        GameManager.inBattle = false;
        GameManager.inGame = false;
        GameManager.newGame = true;
        SceneManager.LoadScene("0.MainScene");
        GameManager.playerStatus = GameManager.playerStatus.SetUnitStatus(UNIT_TYPE.PLAYER);
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }    
}
