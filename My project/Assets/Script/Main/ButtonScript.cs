using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    [Header("메인 인벤토리")]
    public Inventory mainInventory;
    [Header("배틀 인벤토리")]
    public Inventory battleInventory;

    #region 시작화면, 난이도선택, 옵션
    public void LoadMainScene()
    {
        SceneManager.LoadScene("0.MainScene");
        GameManager.newGame = true;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void LoadStoryScene()
    {
        LoadingSceneController.LoadScene("1.StoryScene");
        //SceneManager.LoadScene("1.StoryScene");
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
    public void LoadMainStageScene()
    {
        LoadingSceneController.LoadScene("3.MainStage");
        //SceneManager.LoadScene("3.MainStage");
        GameManager.DifficultySetEasy();        
        GameManager.inGame = true;
        GameManager.inBattle = false;
        GameManager.newGame = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void BackMainScene()
    {
        GameManager.OffInventory();
        GameManager.inBattle = false;
        GameManager.inGame = true;
        GameManager.newGame = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        //SoundManager.bgms[0].Play();
    }     

    public void LoadBattleStage()
    {
        GameManager.OffInventory();
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
        GameManager.difficulty = DIFFICULTY.HARD;
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

    public void LoadRestScene()
    {
        GameManager.button = EventSystem.current.currentSelectedGameObject;
        GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.inBattle = false;
        GameManager.canRest = false;
        GameManager.cameraSelect = CAMERA_TYPE.REST;
    }

    public void LoadSecretRoomScene()
    {
        GameManager.button = EventSystem.current.currentSelectedGameObject;
        GameManager.button.GetComponent<Button>().interactable = true;
        GameManager.inBattle = false;
        GameManager.getKey = false;
        Debug.Log("열쇠를 지워라");
        mainInventory.DeleteItem("SecretKey");
        battleInventory.DeleteItem("SecretKey");
        GameManager.cameraSelect = CAMERA_TYPE.SECRET;
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
        GameManager.inBattle = false;
        GameManager.newGame = false;
        GameManager.DifficultySetNormal();        
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion

    #region HardStage
    public void LoadHardMainScene()
    {
        SceneManager.LoadScene("7.HardSceneTest");
        GameManager.inGame = true;
        GameManager.inBattle = false;
        GameManager.newGame = false;
        GameManager.DifficultySetHard();
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    #endregion
        
    #region 그 외 기능
    public void LevelPopUpOff()
    {
        GameManager.nowLevelUp = false;    
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
        Time.timeScale = 1;
    }
    #endregion
}

