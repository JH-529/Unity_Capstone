using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadOptionScene()
    {
        SceneManager.LoadScene("OptionScene");
    }

    public void LoadDifficultyScene()
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    public void LoadEasyStageScene()
    {
        SceneManager.LoadScene("EasyScene");
    }

    public void LoadEasyShopScene()
    {
        SceneManager.LoadScene("EasyShop");
    }

    public void LoadNormalStageScene()
    {
        SceneManager.LoadScene("NormalScene");
    }

    public void LoadNormalShopScene()
    {
        SceneManager.LoadScene("NormalShop");
    }

    public void LoadHardStageScene()
    {
        SceneManager.LoadScene("HardScene");
    }

    public void LoadHardShopScene()
    {
        SceneManager.LoadScene("HardShop");
    }
}

