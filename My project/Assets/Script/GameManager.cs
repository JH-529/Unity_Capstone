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
    /*�÷��̾��� ī�� ���*/

    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;

    // GameManager�� SingleTon����
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // ������ �̱��� ������(���� �м� ����)
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
        // ���̵� ���� â���� �̵��Ҷ� ���� NullReference ���� -> ���� ���� �ʿ�
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
