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
        GameManager.playerStatus.hp -= damage;
    }

    public void PlayerHealed(int heal)
    {
        GameManager.playerStatus.hp += heal;
    }

    public void BuyItem(int gold)
    {
        GameManager.playerStatus.gold -= gold;
    }

    public void EarnItem(int gold)
    {
        GameManager.playerStatus.gold += gold;
    }

    
}
