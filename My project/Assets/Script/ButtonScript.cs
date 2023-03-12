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

    public void LoadDifficultyScene()
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    public void LoadNormalStageScene()
    {
        SceneManager.LoadScene("NormalScene");
    }

    public void LoadNormalShopScene()
    {
        SceneManager.LoadScene("NormalShop");
    }
}

