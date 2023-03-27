using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Victory();
        }

        EnemyAttack();
        GameManager.turnStart = true;        
    }

    // ���� ���� �Լ�
    // ���� ���� �� EnemyAttack() �ڵ� ����
    public void GetDefence()
    {
        float defence = GameManager.playerDamage;
        GameManager.playerStatus.defence = defence;
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
            Debug.Log("�й�...");
            GameManager.inBattle = false;
            SceneManager.LoadScene("0.MainScene");
            GameManager.cameraSelect = CAMERA_TYPE.MAIN;            
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
    
}
