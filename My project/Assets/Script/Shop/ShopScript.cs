using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum ITEM_TYPE
{
    HELMET,
    ARMOR,
    KNIFE,
}

public class ShopScript : MonoBehaviour
{
    [Header("메인 인벤토리")]
    public Inventory mainInventory;
    [Header("배틀 인벤토리")]
    public Inventory battleInventory;
    public List<Item> items;

    Button btn;
    public SpriteRenderer sRenderer;
    public GameObject textObject;
    public TextMeshProUGUI textMesh;    

    void Start()
    {
        btn = GameObject.Find("Item1Button").GetComponent<Button>();

        sRenderer = GameObject.Find("Item1Icon").GetComponent<SpriteRenderer>();
        textObject = transform.GetChild(0).gameObject;

        #region Heal & Shield
        if (sRenderer.sprite.name.Equals("Heal"))
        {
            btn.onClick.AddListener(BuyHeal_10);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("WeakShield"))
        {
            btn.onClick.AddListener(BuyShield_10);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("Shield"))
        {
            btn.onClick.AddListener(BuyShield_20);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "20G";
        }
        #endregion

        #region Item
        if (sRenderer.sprite.name.Equals("Helmet"))
        {
            btn.onClick.AddListener(BuyHelmet);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("Armor"))
        {
            btn.onClick.AddListener(BuyArmor);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("Knife"))
        {
            btn.onClick.AddListener(BuyKnife);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        #endregion

        if (sRenderer.sprite.name.Equals("Reinforce"))
        {
            btn.onClick.AddListener(Reinforce);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "30G";
        }
        
    }

    void Update()
    {
        if (GameManager.reinforce == true && sRenderer.sprite.name.Equals("Reinforce"))
        {
            btn.interactable = false;
        }
    }

    #region heal
    public void BuyHeal_10()
    {
        if (GameManager.playerGold >= 10)
        {
            GameManager.playerGold -= 10;
            GameManager.playerStatus.hp += 10;
            if (GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
            { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }
            btn.interactable = false;
        }        
    }

    public void BuyHeal_20()
    {
        if (GameManager.playerGold >= 20)
        {
            GameManager.playerGold -= 20;
            GameManager.playerStatus.hp += 20;
            if (GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
            { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }
            btn.interactable = false;
        }
    }
    public void BuyHeal_30()
    {
        if (GameManager.playerGold >= 30)
        {
            GameManager.playerGold -= 30;
            GameManager.playerStatus.hp += 30;
            if (GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
            { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }
            btn.interactable = false;
        }
    }
    #endregion

    #region shield
    public void BuyShield_10()
    {
        if (GameManager.playerGold >= 10)
        {
            GameManager.playerGold -= 10;
            GameManager.playerStatus.shield += 5;
            btn.interactable = false;
        }
    }
    public void BuyShield_20()
    {
        if (GameManager.playerGold >= 20)
        {
            GameManager.playerGold -= 20;
            GameManager.playerStatus.shield += 10;
            btn.interactable = false;
        }
    }
    public void BuyShield_30()
    {
        if (GameManager.playerGold >= 30)
        {
            GameManager.playerGold -= 30;
            GameManager.playerStatus.shield += 15;
            btn.interactable = false;
        }
    }
    #endregion

    public void Reinforce()
    {
        if (GameManager.playerGold >= 30)
        {
            GameManager.playerGold -= 30;
            GameManager.reinforce = true;
        }
    }

    public void BuyHelmet()
    {
        if (GameManager.playerGold >= 10)
        {
            Debug.Log("헬멧 구입");
            GameManager.playerGold -= 10;
            mainInventory.AddItem(items[(int)ITEM_TYPE.HELMET]);
            battleInventory.AddItem(items[(int)ITEM_TYPE.HELMET]);
            btn.interactable = false;
        }
    }

    public void BuyArmor()
    {
        if (GameManager.playerGold >= 10)
        {
            Debug.Log("아머 구입");
            GameManager.playerGold -= 10;
            mainInventory.AddItem(items[(int)ITEM_TYPE.ARMOR]);
            battleInventory.AddItem(items[(int)ITEM_TYPE.ARMOR]);
            btn.interactable = false;
        }
    }

    public void BuyKnife()
    {
        if (GameManager.playerGold >= 10)
        {
            Debug.Log("나이프 구입");
            GameManager.playerGold -= 10;
            mainInventory.AddItem(items[(int)ITEM_TYPE.KNIFE]);
            battleInventory.AddItem(items[(int)ITEM_TYPE.KNIFE]);
            btn.interactable = false;
        }
    }
}
