using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("0.MainScene");
    }

    public void LoadDifficultyScene()
    {
        SceneManager.LoadScene("1.DifficultyScene");
    }

    public void LoadOptionScene()
    {
        SceneManager.LoadScene("2.OptionScene");
    }
        
    public void LoadEasyStageScene()
    {
        SceneManager.LoadScene("3.EasyScene");
    }

    public void LoadEasyShopScene()
    {
        SceneManager.LoadScene("4.EasyShop");
    }

    public void LoadNormalStageScene()
    {
        SceneManager.LoadScene("5.NormalScene");
    }

    public void LoadNormalShopScene()
    {
        SceneManager.LoadScene("6.NormalShop");
    }

    public void LoadHardStageScene()
    {
        SceneManager.LoadScene("7.HardScene");
    }

    public void LoadHardShopScene()
    {
        SceneManager.LoadScene("8.HardShop");
    }

    public void LoadTestBattleScene()
    {
        SceneManager.LoadScene("9.TestBattleScene");
    }
}

