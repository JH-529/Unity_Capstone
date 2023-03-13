using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static int playerHp = 50;
    public static int playerMaxHp = 50;
    public static int playerGold = 10;
    /*플레이어의 카드 목록*/

    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;

    // GameManager의 SingleTon패턴
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!gameManager)
            {
                gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (gameManager == null)
                    Debug.Log("no Singleton obj");
            }
            return gameManager;
        }
    }

    void Awake()
    {  
        // 실패한 싱글톤 디자인(이후 분석 예정)
        //if (gameManager != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //gameManager = this;
        //DontDestroyOnLoad(gameObject);
                
    }

    // Start is called before the first frame update
    void Start()
    {
        // 난이도 선택 창으로 이동할때 마다 NullReference 오류 -> 로직 변경 필요
        if(GameObject.Find("HPText").GetComponent<TextMeshProUGUI>())
            UIhp = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
        if(GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>())
            UIGold = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (UIhp != null && UIGold != null)
        {
            UIhp.text = playerHp + " / " + playerMaxHp;
            UIGold.text = playerGold + " G";
        }
    }
}
