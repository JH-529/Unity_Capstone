using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopUp : MonoBehaviour
{
    public GameObject levelPopUp;
    [SerializeField] TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        //levelPopUp = GameObject.Find("LevelPopUp");
        levelText = GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>();
        levelPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.nowLevelUp == true)
        {
            levelPopUp.SetActive(true);
            levelText.text = "레벨업!\n" + "이제" + GameManager.playerLevel + " 레벨";
            Time.timeScale =0;
        }

        if (BattleScript.killBoss == true)
        {
            levelPopUp.SetActive(true);
            levelText.text = "게임 클리어!";
            Time.timeScale = 0;
        }
    }
}
