using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void LoadBattleStage()
    {
        GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
    }

    public void LoadShopScene()
    {
        GameManager.cameraSelect = CAMERA_TYPE.SHOP;
    }

    //public void LoadEasyStage1()
    //{
    //    //SceneManager.LoadScene("3-1.EasyBattle-1");
    //    GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
    //}
    //public void LoadEasyStage2()
    //{
    //    //SceneManager.LoadScene("3-2.EasyBattle-2");
    //    GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
    //}
    //public void LoadEasyStage3()
    //{
    //    //SceneManager.LoadScene("3-3.EasyBattle-3");
    //    GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
    //}
    //public void LoadEasyStage4()
    //{
    //    //SceneManager.LoadScene("3-4.EasyBattle-4");
    //    GameManager.cameraSelect = CAMERA_TYPE.BATTLE;
    //}      
   
#endregion ~Easy난이도 스테이지 모음

#region NormalStage
    public void LoadNormalMainScene()
    {
        SceneManager.LoadScene("5.NormalSceneTest");
        GameManager.DifficultySetNormal();
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    //public void LoadNormalStage1()
    //{
    //    SceneManager.LoadScene("5-1.NormalBattle-1");
    //}
    //public void LoadNormalStage2()
    //{
    //    SceneManager.LoadScene("5-2.NormalBattle-2");
    //}
    //public void LoadNormalStage3()
    //{
    //    SceneManager.LoadScene("5-3.NormalBattle-3");
    //}
    //public void LoadNormalStage4()
    //{
    //    SceneManager.LoadScene("5-4.NormalBattle-4");
    //}
    //public void LoadNormalShopScene()
    //{
    //    SceneManager.LoadScene("6.NormalShop");
    //}
    #endregion ~Normal난이도 스테이지 모음

    #region HardStage
    public void LoadHardMainScene()
    {
        SceneManager.LoadScene("7.HardSceneTest");
        GameManager.DifficultySetHard();
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    //public void LoadHardStage1()
    //{
    //    SceneManager.LoadScene("7-1.HardBattle-1");
    //}
    //public void LoadHardStage2()
    //{
    //    SceneManager.LoadScene("7-2.HardBattle-2");
    //}
    //public void LoadHardStage3()
    //{
    //    SceneManager.LoadScene("7-3.HardBattle-3");
    //}
    //public void LoadHardStage4()
    //{
    //    SceneManager.LoadScene("7-4.HardBattle-4");
    //}
    //public void LoadHardShopScene()
    //{
    //    SceneManager.LoadScene("8.HardShop");
    //}
    #endregion ~Hard난이도 스테이지 모음

    public void LoadTestBattleScene()
    {
        SceneManager.LoadScene("9.TestBattleScene");
    }
}

