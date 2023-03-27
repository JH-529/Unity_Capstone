using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    

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
}
