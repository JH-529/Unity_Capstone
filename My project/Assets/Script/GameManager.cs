using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int  playerHp;
    public int  playerGold;
    /*플레이어의 카드 목록*/

    public TextMeshProUGUI UIhp;
    public TextMeshProUGUI UIGold;
   
    void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
