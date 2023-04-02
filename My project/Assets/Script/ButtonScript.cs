using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    #region 시작화면, 난이도선택, 옵션
    public void LoadMainScene()
    {
        SceneManager.LoadScene("0.MainScene");
        GameManager.newGame = true;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void LoadDifficultyScene()
    {
        SceneManager.LoadScene("1.DifficultyScene");
        GameManager.inGame = false;        
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void LoadOptionScene()
    {
        SceneManager.LoadScene("2.OptionScene");
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion

    #region EasyStage        
    public void LoadEasyMainScene()
    {  
        SceneManager.LoadScene("3.EasySceneTest");
        GameManager.DifficultySetEasy();        
        GameManager.inGame = true;
        GameManager.inBattle = false;
        GameManager.newGame = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void BackMainScene()
    {
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        //SoundManager.bgms[0].Play();
    }     

    public void LoadBattleStage()
    {
        GameManager.button = EventSystem.current.currentSelectedGameObject;
        GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.inBattle = true;
        GameManager.inboss = false;
        GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
        GameManager.playerStatus.defence += GameManager.playerStatus.shield;
        GameManager.playerStatus.shield = 0;
        //SoundManager.bgms[1].Play();
    }

    public void LoadBossStage()
    {
        GameManager.button = EventSystem.current.currentSelectedGameObject;
        GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.inBattle = true;
        GameManager.inboss = true;
        GameManager.enemyStatus.maxHp *= 2;
        GameManager.enemyStatus.hp *= 2;
        GameManager.enemyStatus.defence *= 2;
        GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
        GameManager.playerStatus.defence += GameManager.playerStatus.shield;
        GameManager.playerStatus.shield = 0;
        //SoundManager.bgms[1].Play();
    }

    public void LoadSpecialScene()
    {
        if (GameManager.playerGold < 20)
        { Debug.Log("골드가 부족합니다"); }
        else
        {
            GameManager.button = EventSystem.current.currentSelectedGameObject;
            GameManager.button.GetComponent<Button>().interactable = false;
            GameManager.inBattle = false;
            GameManager.cameraSelect = CAMERA_TYPE.SPECIAL;
        }        
    }

    public void LoadShopScene()
    {
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.SHOP;
    }       
#endregion

    #region NormalStage
    public void LoadNormalMainScene()
    {
        SceneManager.LoadScene("5.NormalSceneTest");
        GameManager.inGame = true;
        GameManager.DifficultySetNormal();        
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion

    #region HardStage
    public void LoadHardMainScene()
    {
        SceneManager.LoadScene("7.HardSceneTest");
        GameManager.inGame = true;
        GameManager.DifficultySetHard();
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion

    
}

