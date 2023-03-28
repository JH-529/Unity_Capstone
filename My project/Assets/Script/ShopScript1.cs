using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript1 : MonoBehaviour
{
    Button btn;
    public SpriteRenderer sRenderer;
    public GameObject textObject;
    public TextMeshProUGUI textMesh;

    void Start()
    {
        btn = GameObject.Find("Item2Button").GetComponent<Button>();

        sRenderer = GameObject.Find("Item2Icon").GetComponent<SpriteRenderer>();
        textObject = transform.GetChild(0).gameObject;

        if (sRenderer.sprite.name.Equals("Heal"))
        {
            btn.onClick.AddListener(BuyHeal_10);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("WeakShield"))
        {
            btn.onClick.AddListener(BuyDefence_10);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "10G";
        }
        if (sRenderer.sprite.name.Equals("Shield"))
        {
            btn.onClick.AddListener(BuyDefence_20);
            textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = "20G";
        }
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
        }
    }
    #endregion

    #region shield
    public void BuyDefence_10()
    {
        if (GameManager.playerGold >= 10)
        {
            GameManager.playerGold -= 10;
            GameManager.playerStatus.defence += 5;
        }
    }
    public void BuyDefence_20()
    {
        if (GameManager.playerGold >= 20)
        {
            GameManager.playerGold -= 20;
            GameManager.playerStatus.defence += 10;
        }
    }
    public void BuyDefence_30()
    {
        if (GameManager.playerGold >= 30)
        {
            GameManager.playerGold -= 30;
            GameManager.playerStatus.defence += 15;
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
}
