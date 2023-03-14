using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGoldScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamaged(int damage)
    {
        if(GameManager.playerStatus.hp - damage > 0)
            GameManager.playerStatus.hp -= damage;     
    }

    public void PlayerHealed(int heal)
    {
        if(GameManager.playerStatus.hp <= GameManager.playerStatus.maxHp - heal)
           GameManager.playerStatus.hp += heal;
    }

    public void EnemyDamaged(int damage)
    {
        if(GameManager.enemyStatus.hp - damage > 0)
            GameManager.enemyStatus.hp -= damage;
    }

    public void EnemyHealed(int heal)
    {
        if (GameManager.enemyStatus.hp <= GameManager.enemyStatus.maxHp - heal)
            GameManager.enemyStatus.hp += heal;
    }

    public void BuyItem(int gold)
    {
        if(GameManager.playerStatus.gold - gold >= 0)
            GameManager.playerStatus.gold -= gold;
    }

    public void EarnGold(int gold)
    {
        GameManager.playerStatus.gold += gold;
    }

    
}
