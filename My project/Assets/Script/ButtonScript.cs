using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

    public void LoadMainScene()
    {
        SceneManager.LoadScene("0.MainScene");
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


#region EasyStage        
    public void LoadEasyMainScene()
    {  
        SceneManager.LoadScene("3.EasySceneTest");
        GameManager.DifficultySetEasy();        
        GameManager.inGame = true;
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void BackMainScene()
    {
        GameManager.inBattle = false;
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }     

    public void LoadBattleStage()
    {
        GameManager.button = EventSystem.current.currentSelectedGameObject;
        GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.inBattle = true;
        GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
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

    public void LoadTestBattleScene()
    {
        SceneManager.LoadScene("9.TestBattleScene");
    }
}

