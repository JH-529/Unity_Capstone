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
        GameManager.playerHp -= damage;
    }

    public void PlayerHealed(int heal)
    {
        GameManager.playerHp += heal;
    }

    public void BuyItem(int gold)
    {
        GameManager.playerGold -= gold;
    }

    public void EarnItem(int gold)
    {
        GameManager.playerGold += gold;
    }

    
}
