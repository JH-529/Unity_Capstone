using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{
    public void PlayerAttack()
    {
        float damage = GameManager.playerDamage;
        if(damage <= 0)
        { damage = 0; }

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
        { GameManager.enemyStatus.hp = 0; }
        EnemyAttack();
        GameManager.turnStart = true;

        if (GameManager.enemyStatus.hp == 0)
        {
            // ButtonScript.LoadEasyMainScene()작동과 동일
            Debug.Log("승리!");
            SceneManager.LoadScene("3.EasySceneTest");
            GameManager.DifficultySetEasy();
            GameManager.inGame = true;
            GameManager.inBattle = false;
            GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        }
    }

    public void GetDefence()
    {
        float defence = GameManager.playerDamage;
        GameManager.playerStatus.defence = defence;
        EnemyAttack();

        GameManager.turnStart = true;

        if (GameManager.enemyStatus.hp == 0)
        {
            // ButtonScript.LoadEasyMainScene()작동과 동일
            Debug.Log("승리!");
            SceneManager.LoadScene("3.EasySceneTest");
            GameManager.DifficultySetEasy();
            GameManager.inGame = true;
            GameManager.inBattle = false;
            GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        }
    }

    public void EnemyAttack()
    {
        float damage = GameManager.enemyDamage;
        if(damage <= 0)
        { damage = 0; }

        // 방어력이 존재할 경우 방어력 우선 감소
        // 방어가 깨지면(0미만으로 감소) 그때부터 hp 감소
        if(GameManager.playerStatus.defence > 0)
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
            Debug.Log("패배...");
            GameManager.inBattle = false;
            SceneManager.LoadScene("0.MainScene");
            GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        }
    }

    
}
